using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 1f;
    
    public float enemySpawnCooldown;
    public Transform SpawnPoint;
    public GameObject EnemyPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        AstarPath.active.Scan();

        enemySpawnCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        enemySpawnCooldown += Time.deltaTime;
        if (enemySpawnCooldown >= spawnTime)
        {
            enemySpawnCooldown = 0;
            GameObject obj = Instantiate(EnemyPrefab, SpawnPoint.position, Quaternion.identity);
            
        }
    }

    

    
}
