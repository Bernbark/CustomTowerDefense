using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillsShopData : MonoBehaviour
{
    string save;
    private static KillsShopData Instance { get; set; }
    public Text valueModText, currentValueModText, bloodPerSecText;
    public static float valueMod, valueCost;
    public static int bloodPerSecond, bloodPerSecondCost;
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
            bloodPerSecond = int.Parse(values[2]);
            bloodPerSecondCost = int.Parse(values[3]);
        }
        else
        {
            valueCost = 100;
            valueMod = 1;
            bloodPerSecond = 0;
            bloodPerSecondCost = 1000;
        }
        UpdateText();
    }
    // Every time I increment, update text and save
    private void IncrementValue()
    {
        valueMod += .25f;
        valueCost *= 2;
        UpdateText();
        Save();
        
    }
    public static void IncrementValueMod()
    {
        Instance.IncrementValue();
    }
    private void IncrementBloodPerSec()
    {
        bloodPerSecond++;
        bloodPerSecondCost *= 2;
        UpdateText();
        Save();
    }

    public static void IncrementBloodPerSec_Static()
    {
        Instance.IncrementBloodPerSec();
    }

    private void OnApplicationQuit()
    {
        Save();
        
    }

    public void ResetData()
    {
        valueCost = 100;
        valueMod = 1;
        bloodPerSecond = 0;
        bloodPerSecondCost = 1000;
        Save();
        UpdateText();
    }

    public static void ResetData_Static()
    {
        Instance.ResetData();
    }

    public void UpdateText()
    {
        valueModText.text = "Next Increment Costs " + valueCost + " Kills";
        currentValueModText.text = "Enemies are currently worth " + (valueMod * 100) + "% of their usual value in gold";
        bloodPerSecText.text = "Upgrade Blood/Sec by 1 for " + bloodPerSecondCost + " kills";
    }

    public static void UpdateText_Static()
    {
        Instance.UpdateText();
    }
    public void Save()
    {
        save = valueMod.ToString() + "_" + valueCost.ToString() + "_" + bloodPerSecond.ToString() + "_" + bloodPerSecondCost.ToString();
        PlayerPrefs.SetString("KILLS_SHOP", save);
        Debug.Log(save);
    }

    
}
