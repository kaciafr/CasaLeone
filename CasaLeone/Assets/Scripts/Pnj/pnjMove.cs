using System;
using UnityEngine;
using UnityEngine.AI;

namespace Pnj
{
    public class PnjMove : MonoBehaviour,IInteract
    {
        public enum Cycle
        {
            Waiting,
            Reflexion,
            Command,
            Timer,
            Check,
            Exit
        }
        public Cycle logic;

        private AllPlace place;
        private ListMove moveAtTheList;
        private MenuOfTheRestaurant menu;
        private AngoisseBar angoisseBar;
        private Inventory playerInventory;
    
        [Header("References")]
        public Transform target;
        [SerializeField] private GameObject player;
        [SerializeField] private NavMeshAgent agent;
    
        [Header("Settings")]
        [SerializeField] private float updateSpeed = 0.1f;
        [SerializeField] private float reflexionTime;
        public float whaitingTime;
        public float eatTime;
    
        public Ingrediente whatTheyWhant;
        public Action<GameObject> PlayerisSit;
        public Action<PnjMove> ClientWhantTakeOrder;
        public Action<PnjMove> PlayerTakeOrder;
        public Action<PnjMove> ClientWhait;
        public Action<PnjMove> PlayerGiveTheOrder;
        public Action<PnjMove> ClientWhantTheCheck;
        public Action<PnjMove> PlayerTakeTheCheck;
        public Action<PnjMove> ClientLeave;
    
        private float delayTime;
        private float delayTimes;
        private float delayeatTimes;
        private bool isCommanding = true;
        private bool hasArrived = true;
        private bool isEating = true;
        private bool whantLeave = true;
        private bool takeTheOrder = false;
        public PnjIn mySeat;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            place = AllPlace.Instance;
            moveAtTheList = ListMove.Instance;
            menu = MenuOfTheRestaurant.Instance;
            playerInventory = Inventory.Instance;
        
        }

        private void Start()
        {
            delayTime = whaitingTime;
            delayTimes = reflexionTime;
            delayeatTimes = eatTime;
            target = place.outSide.transform;
        }
        private void Update()
        {
            switch (logic)
            {
                case Cycle.Waiting:
                    Waiting();
                    break;
                case Cycle.Reflexion:
                    Reflexion();
                    break;
                case Cycle.Command:
                    Command();
                    break;
                case Cycle.Timer:
                    Timer();
                    break;
                case Cycle.Check:
                    Check();
                    break;
                case Cycle.Exit:
                    Exit();
                    break;
            }
        }
        private void Waiting()
        {
            if (target == place.outSide.transform)
            {
                Transform t = place.FindPlace(player);
                target = t;
                mySeat = t.GetComponent<PnjIn>();
                agent.SetDestination(t.position);
            }
        
            if(target.transform != place.outSide.transform && 
               agent.remainingDistance < agent.stoppingDistance &&
               agent.velocity.magnitude > 0.5f)
            {
                PlayerisSit?.Invoke(player);
                logic =  Cycle.Reflexion;
            }
        }
        private void Reflexion()
        {
            delayTimes-=Time.deltaTime;
            if (delayTimes < 0 && isCommanding)
            {
                isCommanding = false;
                ClientWhantTakeOrder?.Invoke(this);
            }
        }
        private void Command()
        {
            menu.StartTakeOrder(this);
        }
        private void Timer()
        {
            delayTime -= Time.deltaTime;
            ClientWhait?.Invoke(this);
            if (delayTime <= 0)
            {
                logic = Cycle.Exit;
                angoisseBar.AddAnguish(5f);
            }
        }
        private void Check()
        {
            delayeatTimes -= Time.deltaTime;
            if (delayeatTimes < 0 && isEating)
            {
                ClientWhantTheCheck?.Invoke(this);
                isEating = false;
                angoisseBar.RemoveAnguish(2f);
            }
        }
        private void Exit()
        {
            if (hasArrived)
            {
                Leave();
                target = place.exit.transform;
                agent.SetDestination(target.position);
                hasArrived = false;
                ClientLeave?.Invoke(this);
            }

            if (agent.remainingDistance < agent.stoppingDistance && agent.velocity.magnitude > 0.5f )
            {
                Destroy(gameObject);
            }
        }

        public void Interact()
        {
            if (logic == Cycle.Reflexion)
            {
                takeTheOrder =  true;
                PlayerTakeOrder?.Invoke(this);
                logic = Cycle.Command;
            }

            if (logic == Cycle.Timer)
            {
                foreach (var ingrediente in playerInventory.ingredientes)
                {
                    if (ingrediente == whatTheyWhant)
                    {
                        PlayerGiveTheOrder?.Invoke(this);
                        playerInventory.RemoveIngrediente(ingrediente);
                        moveAtTheList.Remove(ingrediente); // event
                        logic = Cycle.Check;
                    }
                }
            }

            if (logic==Cycle.Check)
            {
                PlayerTakeTheCheck?.Invoke(this);
                logic = Cycle.Exit;
            }
        }

        private void Leave()
        {
            if (mySeat != null)
            {
                mySeat.Leave();
            }
        }

        public void EndInteraction()
        {
        }
    }
}
