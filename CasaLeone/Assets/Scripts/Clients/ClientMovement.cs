using System;
using Clients.States;
using Interaction;
using Inventories;
using ListForEat;
using PnjWaves;
using UnityEngine;
using UnityEngine.AI;

namespace Clients
{
    public enum EClientState
    {
        Waiting,
        Reflexion,
        Command,
        Timer,
        Check,
        Exit
    }
    
    public class ClientMovement : MonoBehaviour,IInteractable
    {
        public EClientState logic;

        private AllPlace place;
        private Restaurant menu;
        private AngoisseBar.AngoisseBar angoisseBar;
        private Inventory playerInventory;
        private ListOfCommand listOfCommand;
    
        [Header("References")]
        public Transform target;
        public ClientTypeSO clientData;
        [SerializeField] private GameObject player;
        [SerializeField] private NavMeshAgent agent;
        
        [Header("Settings")]
        [SerializeField] private float updateSpeed = 0.1f;
        [SerializeField] private float reflexionTime;
        public float whaitingTime;
        public float eatTime;
    
        public Ingrediente whatTheyWhant;
        public Action<GameObject> PlayerisSit;
        public Action<ClientMovement> ClientWhantTakeOrder;
        public Action<ClientMovement> PlayerTakeOrder;
        public Action<ClientMovement> ClientWhait;
        public Action<ClientMovement> PlayerGiveTheOrder;
        public Action<ClientMovement> ClientWhantTheCheck;
        public Action<ClientMovement> PlayerTakeTheCheck;
        public Action<ClientMovement> ClientLeave;
    
        private float delayTime;
        private float delayTimes;
        private float delayeatTimes;
        private bool isCommanding = true;
        private bool hasArrived = true;
        private bool isEating = true;
        private bool whantLeave = true;
        private bool takeTheOrder = false;
        public ClientSeat mySeat;

        public ClientController Controller { get; private set; }
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            place = AllPlace.Instance;
            menu = Restaurant.Instance;
            playerInventory = Inventory.Instance;
            listOfCommand = ListOfCommand.Instance;
        }

        private void Start()
        {
            delayTime = whaitingTime;
            delayTimes = reflexionTime;
            delayeatTimes = eatTime;
            target = place.outSide.transform;
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
        
        
        private void Command()
        {
            menu.StartTakeOrder(this);
            listOfCommand.UpdateVisuel(this);
        }
        private void Timer()
        {
            delayTime -= Time.deltaTime;
            ClientWhait?.Invoke(this);
            if (delayTime <= 0)
            {
                logic = EClientState.Exit;
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
            if (logic == EClientState.Reflexion)
            {
                takeTheOrder =  true;
                PlayerTakeOrder?.Invoke(this);
                
                logic = EClientState.Command;
            }

            if (logic == EClientState.Timer)
            {
                foreach (var ingrediente in playerInventory.ingredientes)
                {
                    if (ingrediente == whatTheyWhant)
                    {
                        PlayerGiveTheOrder?.Invoke(this);
                        playerInventory.RemoveIngrediente(ingrediente);
                        listOfCommand.Remove(this);
                        logic = EClientState.Check;
                    }
                }
            }

            if (logic==EClientState.Check)
            {
                PlayerTakeTheCheck?.Invoke(this);
                logic = EClientState.Exit;
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
