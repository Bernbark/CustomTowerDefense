using UnityEngine;



public class MohawkMan : EnemyBehavior
{
    protected override void Start()
    {
        base.Start();
        hp = (20 * playerLevel) + playerKills / 1000;
        value = 2 * playerKills;
        maxHP = hp;
        Debug.Log("fish hp" + hp);
    }

    void Update()
    {
        if (hp <= 0)
        {
            hp = 0;
            player.AddGold((int)value);
            player.AddXP(3);
            player.AddToKillCount();
            Destroy(this.gameObject);

        }
    }
}
