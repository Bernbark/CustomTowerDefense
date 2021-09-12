using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour
{
    [SerializeField] private GameObject cannonTip;
    float distanceToEnemy;
    public TurretBehavior tower;
    [SerializeField] private float maxSearchDistance;
    float degreesPerSec = 360f;
    EnemyBehavior closestEnemy = null;
    private bool targetFound = false;
    public Transform gun;
    // Update is called once per frame
    private void Start()
    {
       
        
        
    }
    void Update()
    {
        
        FindClosestEnemy();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxSearchDistance);
    }

    void FindClosestEnemy()
    {
        maxSearchDistance = tower.GetRange();
        float distanceToClosestEnemy = Mathf.Infinity;
        
        EnemyBehavior[] enemiesOnScreen = GameObject.FindObjectsOfType<EnemyBehavior>();

        foreach(EnemyBehavior currentEnemy in enemiesOnScreen)
        {
            distanceToEnemy = (currentEnemy.transform.position - this.transform.position).magnitude;
            
            if(distanceToEnemy < distanceToClosestEnemy && distanceToEnemy <= maxSearchDistance)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
                targetFound = true;
            }
            
        }
        
        if (closestEnemy != null)
        {
            Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
            AimGun(closestEnemy);
            if (((closestEnemy.transform.position - this.transform.position).magnitude) > maxSearchDistance)
            {
                closestEnemy = null;
                targetFound = false;
            }
        }
        else
        {
            targetFound = false;
            closestEnemy = null;
        }


    }

    public EnemyBehavior GetClosestEnemy()
    {
        return closestEnemy;
    }

    public bool IsTargetAvailable()
    {
        return targetFound;
    }

    private void AimGun(EnemyBehavior closestEnemy)
    {
        Vector3 diff = closestEnemy.transform.position - gun.transform.position;
        diff.Normalize();
        float rot_Z = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(0f, 0f, -rot_Z-270f);
        cannonTip.transform.rotation = gun.rotation;
    }
    public float GetRange()
    {
        return maxSearchDistance;
    }
    public void SetRange(float range)
    {
        this.maxSearchDistance = range;
    }
    public void AddRange(float amount)
    {
        this.maxSearchDistance += amount;
    }
}
