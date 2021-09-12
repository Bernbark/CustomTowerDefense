using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int Gold;
    public int Level;
    public int XPToLevel;
    public int MaxXP;
    public int Kills;

    public PlayerData(Player player)
    {
        Level = player.Level;
        Gold = player.Gold;
        XPToLevel = player.XPToLevel;
        MaxXP = player.MaxXP;
        Kills = player.Kills;
    }
}
