using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public GameObject dartPrefab;
    public BuildingManager buildingManager;
    private Player player;
    public GameObject cannonFirePrefab;

    public float range;
    public float damage;
    public int damageLevel, rangeLevel, specialLevel;

    private float bulletSpeed;

    EnemyBehavior enemy;
    //private int buyAmount;
    public float shootTiming = .5f;
    public float shootCooldown = 0f;
    Animator anim;
    public SpecificObject stats;
    public FindClosest findClosestTool;
    float shortestDistance = 999f;
    [SerializeField] private GameObject cannonTip;
    private GameObject target;
    Bounds bounds;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //AstarPath.active.UpdateGraphs(gameObject.GetComponent<BoxCollider2D>().bounds);
        /**bounds = GetComponent<BoxCollider2D>().bounds;
        bounds.Expand(Vector3.up * 1000);
        var guo = new GraphUpdateObject(bounds);
        guo.updatePhysics = true;
        AstarPath.active.UpdateGraphs(guo);
        */
        AstarPath.active.Scan();
        shootCooldown = Random.Range(0f, .5f);
        player = FindObjectOfType<Player>();
        damage = stats.GetDamage();
        range = stats.GetRange();
        rangeLevel = stats.GetRangeLevel();
        damageLevel = stats.GetDamageLevel();
        specialLevel = 1;
        findClosestTool.SetRange(range);
        
    }

    private void Update()
    {
        shootCooldown += Time.deltaTime;
        if (shootCooldown >= shootTiming)
        {
            
            shootCooldown = 0f;
            if (findClosestTool.IsTargetAvailable())
            {
                enemy = findClosestTool.GetClosestEnemy();
                if (this.tag == "Shotgun")
                {
                    
                    for (int i = 0; i < 10; i++)
                    {
                        bulletSpeed = Random.Range(3f, 7f);
                        
                        Vector3 difference = enemy.transform.position - cannonTip.transform.position;
                        float rotZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
                        float distance = difference.magnitude;
                        Vector2 direction = (difference / distance);
                        direction.Normalize();
                        
                        FireProjectile(direction, rotZ, dartPrefab, bulletSpeed, true, damage);
                    }
                }
                else if (this.tag == "MachineGun")
                {
                    enemy.TakeHit(damage);
                    Destroy(Instantiate(cannonFirePrefab, cannonTip.transform.position, Quaternion.identity), .17f);
                }
                
            } 
        }
    }

    private void OnMouseEnter()
    {
        UpgradeOverlay.Show_Static(this, (rangeLevel * 20 + 10), (damageLevel * 20 + 10));
    }

    private void OnMouseExit()
    {
        UpgradeOverlay.Hide_Static();
        Tooltip.HideTooltip_Static();
    }

    public void UpgradeRange(int buyAmount)
    {
        
        if (player.GetGold() >= (rangeLevel * 20 + 10)*buyAmount)
        {
            range += .5f * buyAmount;
            findClosestTool.AddRange(.5f * buyAmount);
            player.SubtractGold((rangeLevel * 20 + 10)*buyAmount);
            rangeLevel+=buyAmount;
        }
        
    }

    public void UpgradeDamage(int buyAmount)
    {
        if(player.GetGold() >= (damageLevel * 20 + 10)*buyAmount)
        {
            this.damage += 1 * buyAmount;
            player.SubtractGold((damageLevel * 20 + 10)*buyAmount);
            damageLevel+=buyAmount;
        }
        
    }

    public void SetRange(float range)
    {
        this.range = range;
        findClosestTool.SetRange(range);
    }
    public float GetRange()
    {
        return this.range;
    }

    public int GetRangeLevel()
    {
        return this.rangeLevel;
    }

    public void SetRangeLevel(int level)
    {
        this.rangeLevel = level;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public float GetDamage()
    {
        return this.damage;
    }

    public void SetDamageLevel(int level)
    {
        this.damageLevel = level;
    }

    public int GetDamageLevel()
    {
        return this.damageLevel;
    }

    public void DestroyThisProperly()
    {
        //player.AddGold(buildingManager.GetCost());
        stats.DestroySaveable();
        AstarPath.active.UpdateGraphs(bounds);
    }

    void FireProjectile(Vector2 direction, float rotationZ, GameObject bulletPrefab, float bulletSpeed, bool randomized, float damage)
    {
        DartBehavior dart;
        GameObject b = null;
        float randomGenZ = 0f;
        Quaternion newRot = cannonTip.transform.rotation;
        if (randomized)
        {
            float randomGenY = Random.Range(-.3f, .3f);
            float randomGenX = Random.Range(-.3f, .3f);
            randomGenZ = Random.Range(-.7f, .7f);
            float bulletSpeedMod = Random.Range(1f, 3f);
            direction = new Vector2(direction.x, direction.y + randomGenZ);
            direction.Normalize();
            bulletSpeed *= bulletSpeedMod;
            
            b = Instantiate(dartPrefab, cannonTip.transform.position, cannonTip.transform.rotation);
            b.GetComponent<DartBehavior>().spawnOrigin = this;
            
            
            b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
           
        }
        else
        {
            newRot = Quaternion.Euler(cannonTip.transform.eulerAngles.x, cannonTip.transform.eulerAngles.y, cannonTip.transform.eulerAngles.z);
            b = Instantiate(dartPrefab, cannonTip.transform.position, newRot);
            
            // In the case of darts this will be ignored
            b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            
        }
        
    }

   

}
