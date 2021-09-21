using UnityEngine;

public class SadBee : EnemyBehavior
{
    protected override void Start()
    {
        base.Start();
        hp = valueMod * (6 * playerLevel) + playerKills / 1000;
        value = (playerKills / 5) * valueMod;
        maxHP = hp;

    }

    void Update()
    {
        if (hp <= 0)
        {
            hp = 0;
            player.AddGold((int)value);
            player.AddXP(1);
            player.AddToKillCount(BloodShopData.killsMod);
            Destroy(this.gameObject);

        }
    }
}
