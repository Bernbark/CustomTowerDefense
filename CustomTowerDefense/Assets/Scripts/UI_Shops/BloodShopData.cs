using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodShopData : MonoBehaviour
{
    string save;
    public Player player;
    private static BloodShopData Instance { get; set; }
    public Text killsModText, currentBloodText;
    public static float killsModCost;
    public static int killsMod;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("BLOOD_SHOP"))
        {
            // 0 is valueMod, 1 is valueCost
            List<string> values = new List<string>(PlayerPrefs.GetString("BLOOD_SHOP").Split('_'));
            killsMod = int.Parse(values[0]);
            killsModCost = float.Parse(values[1]);
        }
        else
        {
            killsModCost = 100;
            killsMod = 1;
        }
        UpdateText();
    }
    
    public void UpdateText()
    {
        killsModText.text = "Next Kill Upgrade Costs " + killsModCost + " Blood Points\n"+
        "Enemies are currently worth " + killsMod + "kill(s) / death";
        currentBloodText.text = player.GetBlood().ToString() +" blood";
    }

    public static void UpdateText_Static()
    {
        Instance.UpdateText();
    }

    public static void IncrementValueMod()
    {
        Instance.IncrementValue();
    }

    private void IncrementValue()
    {
        killsMod++;
        killsModCost *= 2;
        UpdateText();
        Save();
    }

    private void Save()
    {
        save = killsMod.ToString() + "_" + killsModCost.ToString();
        PlayerPrefs.SetString("BLOOD_SHOP", save);
    }

    private void ResetData()
    {
        killsModCost = 100;
        killsMod = 1;
        UpdateText();
        Save();
    }

    public static void ResetData_Static()
    {
        Instance.ResetData();
    }
}
