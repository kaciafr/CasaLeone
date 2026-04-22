using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range;
    public Transform centrePoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh) return;

        transform.position = new Vector3(
            transform.position.x, 
            transform.position.y, 
            0f
        );

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                agent.SetDestination(point);
            }
        }
    }


    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector2 randomCircle = Random.insideUnitCircle * range;
        Vector3 randomPoint = new Vector3(
            center.x + randomCircle.x,
            center.y + randomCircle.y,
            0f
        );

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = new Vector3(hit.position.x, hit.position.y, 0f);
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}