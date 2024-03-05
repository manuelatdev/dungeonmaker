using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptNavMesh : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject target;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        agent.SetDestination(target.transform.position);
    }
}
