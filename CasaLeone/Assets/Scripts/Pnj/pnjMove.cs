using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class pnjMove : MonoBehaviour,IInteract
{
    public enum cycle
    {
        Waiting,
        Reflexion,
        Command,
        Timer,
        Check,
        Exit
    }
    public cycle logic;
    
    [Header("References")]
    public Transform target;
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AllPlace place;
    [SerializeField] private menuOfTheRestaurant menu;
    [SerializeField] private ListMove moveAtTheList;
    [SerializeField] private Inventory playerInventory;
    
    [Header("Settings")]
    [SerializeField] private float updateSpeed = 0.1f;
    [SerializeField] private float whaitingTime;
    [SerializeField] private float reflexionTime;
    [SerializeField] private float eatTime;
    
    public Ingrediente whatTheyWhant;
    public Action<GameObject> PlayerisSit;
    public Action<pnjMove> ClientWhantTakeOrder;
    public Action<pnjMove> PlayerTakeOrder;
    public Action<pnjMove> ClientWhait;
    public Action<pnjMove> PlayerGiveTheOrder;
    public Action<pnjMove> ClientWhantTheCheck;
    public Action<pnjMove> PlayerTakeTheCheck;
    public Action<pnjMove> ClientLeave;
    
    private float delayTime;
    private float delayTimes;
    private float delayeatTimes;
    private bool isCommanding = true;
    private bool hasArrived = true;
    private bool isEating = true;
    private bool whantLeave = true;
    private bool takeTheOrder = false;
    public pnjIN mySeat;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    private void Start()
    {
        delayTime = whaitingTime;
        delayTimes = reflexionTime;
        delayeatTimes = eatTime;
    }
    private void Update()
    {
        switch (logic)
        {
            case cycle.Waiting:
                Waiting();
                break;
            case cycle.Reflexion:
                Reflexion();
                break;
            case cycle.Command:
                Command();
                break;
            case cycle.Timer:
                Timer();
                break;
            case cycle.Check:
                Check();
                break;
            case cycle.Exit:
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
            mySeat = t.GetComponent<pnjIN>();
            agent.SetDestination(t.position);
        }
        
        if(target.transform != place.outSide.transform && 
           agent.remainingDistance < agent.stoppingDistance &&
           agent.velocity.magnitude > 0.5f)
        {
            PlayerisSit?.Invoke(player);
            logic =  cycle.Reflexion;
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
                logic = cycle.Exit;
                //Add Anguish
            }
    }
    private void Check()
    {
        delayeatTimes -= Time.deltaTime;
        if (delayeatTimes < 0 && isEating)
        {
            ClientWhantTheCheck?.Invoke(this);
            isEating = false;
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
    }

    public void Interact()
    {
        if (logic == cycle.Reflexion)
        {
            takeTheOrder =  true;
            PlayerTakeOrder?.Invoke(this);
            logic = cycle.Command;
        }

        if (logic == cycle.Timer)
        {
            foreach (var ingrediente in playerInventory.ingredientes)
            {
                if (ingrediente == whatTheyWhant)
                {
                    PlayerGiveTheOrder?.Invoke(this);
                    playerInventory.RemoveIngrediente(ingrediente);
                    moveAtTheList.Remove(ingrediente);
                    logic = cycle.Check;
                }
            }
        }

        if (logic==cycle.Check)
        {
            PlayerTakeTheCheck?.Invoke(this);
            logic = cycle.Exit;
        }
    }
    void Leave()
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
