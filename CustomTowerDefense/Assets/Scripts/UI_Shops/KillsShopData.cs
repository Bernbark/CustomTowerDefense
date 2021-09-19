using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillsShopData : MonoBehaviour
{
    string save;
    private static KillsShopData Instance { get; set; }
    public Text valueModText, currentValueModText;
    public static float valueMod, valueCost;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("KILLS_SHOP"))
        {
            // 0 is valueMod, 1 is valueCost
            List<string> values = new List<string>(PlayerPrefs.GetString("KILLS_SHOP").Split('_'));
            valueMod = float.Parse(values[0]);
            valueCost = float.Parse(values[1]);
        }
        else
        {
            valueCost = 100;
            valueMod = 1;
        }
        UpdateText();
    }
    private void IncrementValue()
    {
        valueMod += .25f;
        valueCost *= 2;
        UpdateText();
        save = valueMod.ToString() + "_" + valueCost.ToString();
        PlayerPrefs.SetString("KILLS_SHOP", save);
    }
    public static void IncrementValueMod()
    {
        Instance.IncrementValue();
    }

    private void OnApplicationQuit()
    {
        save = valueMod.ToString() + "_" + valueCost.ToString();
        PlayerPrefs.SetString("KILLS_SHOP", save);
    }

    public void UpdateText()
    {
        valueModText.text = "Next Increment Costs " + valueCost + " Kills";
        currentValueModText.text = "Enemies are currently worth " + (valueMod * 100) + "% of their usual value in gold";
    }

    public static void UpdateText_Static()
    {
        Instance.UpdateText();
    }
}
