using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    Player player;
    private float hp;
    private float maxHP;
    private float value;
    public HealthBar healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.Find("Player").GetComponent<Player>();
        EnemyManager.Instance.enemies.Add(this);
        hp = 5 * player.GetLevel()+(player.GetKills()*Time.deltaTime);
        value = 5 * player.GetLevel() + player.GetKills();
        maxHP = hp;
        Debug.Log(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            hp = 0;
            player.AddGold((int)value);
            player.AddXP(1);
            player.AddToKillCount();
            Destroy(this.gameObject);

        }
    }

    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.transform.tag == "EndPoint")
        {
            
            Destroy(this.gameObject);
            
            player.SubtractGold((int)value);
            EnemyManager.Instance.enemies.Remove(this);
        }
    }

    public void TakeHit(float damage)
    {
        hp -= damage;
        
        healthBar.SetHealth(hp, maxHP);
    }
}
