using UnityEngine;

public class AngryBee : EnemyBehavior
{
    protected override void Start()
    {
        base.Start();
        hp = valueMod * (7 * playerLevel) + playerKills / 1000;
        value = (playerKills / 4 ) *valueMod;
        maxHP = hp;

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
