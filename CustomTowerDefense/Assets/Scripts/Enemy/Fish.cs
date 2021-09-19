

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : EnemyBehavior
{
    protected override void Start()
    {
        base.Start();
        hp = valueMod*(10 * playerLevel) + playerKills / 1000;
        value = (7 + playerKills) * valueMod;
        maxHP = hp;
        Debug.Log("fish hp" + hp);
    }
    
    void Update()
    {
        if (hp <= 0)
        {
            hp = 0;
            player.AddGold((int)value);
            player.AddXP(2);
            player.AddToKillCount();
            Destroy(this.gameObject);

        }
    }
}
