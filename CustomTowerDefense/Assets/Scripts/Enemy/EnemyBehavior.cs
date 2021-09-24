using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    protected float valueMod;
    
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
           
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.transform.tag == "EndPoint")
        {            
            Destroy(this.gameObject);
            
            player.SubtractGoldFromLeak((int)value);
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
