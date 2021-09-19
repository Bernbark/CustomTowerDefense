using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    protected float valueMod;
    private GameObject playerObj;
    protected Player player;
    public float hp;
    public float maxHP;
    public float value;
    public HealthBar healthBar;
    public int playerLevel, playerKills;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerLevel = GetPlayerLevel();
        playerKills = GetPlayerKills();
        hp = 5;
        maxHP = hp;
        value = hp;
        valueMod = KillsShopData.valueMod;
        EnemyManager.Instance.enemies.Add(this);
  
        
    }

    // Update is called once per frame
    

    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.transform.tag == "EndPoint")
        {
            
            Destroy(this.gameObject);
            
            player.SubtractGoldFromLeak((int)value);
            EnemyManager.Instance.enemies.Remove(this);
        }
    }

    public void TakeHit(float damage)
    {
        hp -= damage;
        
        healthBar.SetHealth(hp, maxHP);
    }

    private int GetPlayerLevel()
    {
        return player.GetLevel();
    }

    private int GetPlayerKills()
    {
        return player.GetKills();
    }
}
