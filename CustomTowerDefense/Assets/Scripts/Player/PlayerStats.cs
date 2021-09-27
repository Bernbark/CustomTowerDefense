using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public DatabaseAccess database;
    public Player player;
    private static PlayerStats _instance;

    public static PlayerStats Instance { get { return _instance; } }

    private string towers;

    public string Name;
    public int Gold;
    public int Level;
    public int XPToLevel;
    public int MaxXP;
    public int Kills;
    public int Blood;

    private string save;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        towers = "";
        save = "";
    }

    public void Save()
    {
        FormSaveString();
        database.SaveToDatabase(save);
        towers = "";
        save = "";
    }

    public void SaveTowerLocations(string towerInfo)
    {
        
        
        
        
        
        towers += towerInfo +"_";
    }

    void FormSaveString()
    {
        SetAllStats(player);
        save = Gold + "_" + Level + "_" + XPToLevel + "_" + MaxXP + "_" + Kills + "_" + Blood +"_"+ BloodShopData.killsMod +"_"+ BloodShopData.killsModCost +
            "_"+ KillsShopData.valueCost +"_"+ KillsShopData.valueMod +"_"+KillsShopData.bloodPerSecond+"_"+PublicRelationsBehavior.killsPerSecond+
            "_"+PublicRelationsBehavior.killsPerSecondCost + "_" + towers;

        
    }

    void SetAllStats(Player player)
    {
        Level = player.Level;
        Gold = player.Gold;
        XPToLevel = player.XPToLevel;
        MaxXP = player.MaxXP;
        Kills = player.Kills;
        Blood = player.Blood;
        Name = player.Name;
    }
}
