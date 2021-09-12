using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [SerializeField] private Transform mMovePositionTransform;
    private NavMeshAgent mNavMeshAgent;
    // Start is called before the first frame update
    void Awake()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        mNavMeshAgent.destination = mMovePositionTransform.position;
    }
}
