using UnityEngine;

public class Slime : EnemyBehavior
{
    protected override void Start()
    {
        base.Start();
        hp = valueMod * (30 * playerLevel) + playerKills / 1000;
        value = (2 + playerLevel) * playerKills * valueMod;
        maxHP = hp;
        Debug.Log("fish hp" + hp);
    }

    void Update()
    {
        if (hp <= 0)
        {
            hp = 0;
            player.AddGold((int)value);
            player.AddXP(4);
            player.AddToKillCount();
            Destroy(this.gameObject);

        }
    }
}
