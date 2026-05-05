using Clients;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class LioneMovement : MonoBehaviour
    {
        [field: SerializeField]
        public LioneController Controller { get; private set; }
        private Animator animator;
        [SerializeField] private NavMeshAgent agent;
        private float lastDirectionX = 1f; 


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            animator =GetComponent<Animator>();
        }


        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
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