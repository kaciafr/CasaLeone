using Clients;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class LioneMovement : MonoBehaviour
    {
        [field: SerializeField]
        public ClientController Controller { get; private set; }
        private Animator animator;
        [SerializeField] private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            bool isMoving = agent.velocity.magnitude > 0.1f;
            animator.SetBool("isMoving", isMoving);
        }
        
        public void ClearDestination()
        {
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
        
        public bool HasReachedDestination()
        {
            return !agent.pathPending 
                   && agent.remainingDistance <= agent.stoppingDistance;
        }
    }
}