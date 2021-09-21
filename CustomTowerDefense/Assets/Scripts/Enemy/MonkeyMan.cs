using UnityEngine;

public class MonkeyMan : EnemyBehavior
{
    protected override void Start()
    {
        base.Start();
        hp = valueMod * (50 * playerLevel) + playerKills / 1000;
        value = 4 * playerKills * valueMod;
        maxHP = hp;
        Debug.Log("fish hp" + hp);
    }

    void Update()
    {
        if (hp <= 0)
        {
            hp = 0;
            player.AddGold((int)value);
            player.AddXP(5);
            player.AddToKillCount(BloodShopData.killsMod);
            Destroy(this.gameObject);

        }
    }
}