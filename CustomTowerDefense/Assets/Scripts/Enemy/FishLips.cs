using UnityEngine;

public class FishLips : EnemyBehavior
{
    protected override void Start()
    {
        base.Start();
        hp = (40 * playerLevel) + playerKills / 1000;
        value = 3 * playerKills;
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
            player.AddToKillCount();
            Destroy(this.gameObject);

        }
    }
}
