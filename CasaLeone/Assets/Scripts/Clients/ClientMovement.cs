
using Unity.AI.Navigation;
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

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        
        public void ClearDestination()
        {
            agent.destination = transform.position;
        }
        
        public void SetDestination(Transform destination)
        { 
            agent.SetDestination(destination.position);
        }

        public void SetDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }
        public bool HasArrived()
        {
            return agent.remainingDistance <= agent.stoppingDistance;
        }
        public void AllerA(Vector3 destination) 
        {
            // On récupère l'agent et on lui donne l'ordre
            UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.SetDestination(destination);
        }
    }
}
