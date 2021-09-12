using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;

    public List<EnemyBehavior> enemies { get; set; }

    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<EnemyManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        enemies = new List<EnemyBehavior>();
    }
}
