using System;
using Clients.States;
using Players;
using PnjWaves;
using Restaurants;
using Restaurants.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Clients
{
    public class ClientMovement : MonoBehaviour
    {
        [Header("References")]
        [field: SerializeField]
        public ClientController Controller { get; private set; }
        
        [SerializeField]
        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
        }
        
        public void ClearDestination()
        {
            agent.destination = transform.position;
        }
        
        public void SetDestination(Transform destination)
        {
            SetDestination(destination.position);
        }

        public void SetDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public bool HasArrived()
        {
            return agent.remainingDistance <= agent.stoppingDistance;
        }
    }
}
