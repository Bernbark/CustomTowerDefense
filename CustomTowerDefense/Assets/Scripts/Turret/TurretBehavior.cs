using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public GameObject cannonFirePrefab;
    public float damage;
    EnemyBehavior enemy;
    public float range;
    public float shootTiming = .5f;
    public float shootCooldown = 0f;
    Animator anim;
    public SpecificObject stats;
    public FindClosest findClosestTool;
    float shortestDistance = 999f;
    [SerializeField] private GameObject cannonTip;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        damage = stats.GetDamage();
        range = stats.GetRange();
        findClosestTool.SetRange(range);
        Bounds bounds = gameObject.GetComponent<BoxCollider2D>().bounds;
        bounds.Expand(Vector3.forward * 1000);
        // Update the graph with each object's collider as it loads in
        AstarPath.active.UpdateGraphs(bounds);
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
                enemy.TakeHit(damage);
                Destroy(Instantiate(cannonFirePrefab,cannonTip.transform.position,Quaternion.identity),.17f);
            } 
        }
    }

    private void OnMouseEnter()
    {
        UpgradeOverlay.Show_Static(this);
    }

    private void OnMouseExit()
    {
        UpgradeOverlay.Hide_Static();
    }

    public void UpgradeRange()
    {
        range += .5f;
        findClosestTool.AddRange(.5f);
    }

    public void UpgradeDamage()
    {
        this.damage += 1;
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

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public float GetDamage()
    {
        return this.damage;
    }

    public void DestroyThisProperly()
    {
        stats.DestroySaveable();
    }
}
