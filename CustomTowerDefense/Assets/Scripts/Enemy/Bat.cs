using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyBehavior
{
    
    

    protected override void Start()
    {
        
        
        base.Start();

        hp = ((3 * 1+playerLevel) + playerKills/1000)*valueMod;
        value = (5 + playerKills)*valueMod;
        maxHP = hp;
        Debug.Log("bat hp"+hp);
    }

    void Update()
    {
        if (hp <= 0)
        {
            hp = 0;
            player.AddGold((int)value);
            player.AddXP(1);
            player.AddToKillCount();
            Destroy(this.gameObject);

        }
    }

    
}
