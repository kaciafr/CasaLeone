
using Restaurants.QTESysteme;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace Clients
{
    public class ClientMovement : MonoBehaviour
    {
        [Header("References")]
        [field: SerializeField]
        public ClientController Controller { get; private set; }

        public Animator animator;

        [SerializeField]
        private NavMeshAgent agent;
        
        private float lastDirectionX = 1f; 

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            float speed = new Vector2(agent.velocity.x, agent.velocity.y).magnitude;
    
            animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);

            if (agent.velocity.x > 0.3f)
            {
                lastDirectionX = -1f;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (agent.velocity.x < -0.3f)
            {
                lastDirectionX = 1f;
                transform.localScale = new Vector3(1, 1, 1);
            }
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
            if (agent.pathPending) return false;
            bool hasArrived = agent.remainingDistance <= agent.stoppingDistance;
            return hasArrived;
        }
    }
}
