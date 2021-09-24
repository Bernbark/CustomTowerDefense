
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PublicRelationsBehavior : MonoBehaviour
{
    string save;
    private static PublicRelationsBehavior Instance { get; set; }
    // Collect blood from enemies

    // Collect blood because enemies kill your townsfolk

    // Provide more townsfolk to be eaten (increase birthrate?)

    // Make enemies worth more kills?

    // Kill townsfolk? 
    public Button killsPerSecButton;
    public Text killsPerSecText, killsPerSecCostText;
    public static int killsPerSecond;
    public static int killsPerSecondCost;

    // Breed townsfolk with monsters?

    // Ability to buy the Meat Grinder (collects blood)
    public Player player;
    void Start()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("PUBLIC_RELATIONS_SHOP"))
        {
            // 0 is valueMod, 1 is valueCost
            List<string> values = new List<string>(PlayerPrefs.GetString("PUBLIC_RELATIONS_SHOP").Split('_'));
            killsPerSecond = int.Parse(values[0]);
            killsPerSecondCost = int.Parse(values[1]);
            
        }
        else
        {
            killsPerSecond = 0;
            killsPerSecondCost = 1000000000;
        }
        UpdateText();
        killsPerSecButton.onClick.AddListener(BuyKillsPerSecond);
         
    }

    private void UpdateText()
    {
        killsPerSecCostText.text = "Costs " + killsPerSecondCost + " gold";
        killsPerSecText.text = killsPerSecond + " kills/second";
    }

    private void BuyKillsPerSecond()
    {
        if(player.GetGold() >= killsPerSecondCost)
        {
            player.SubtractGold(killsPerSecondCost);
            killsPerSecond++;
            killsPerSecondCost *= 2;
            UpdateText();
            Save();
        }
    }

    public void Save()
    {
        save = killsPerSecond.ToString() + "_" + killsPerSecondCost.ToString();
        PlayerPrefs.SetString("PUBLIC_RELATIONS_SHOP", save);
    }
}
