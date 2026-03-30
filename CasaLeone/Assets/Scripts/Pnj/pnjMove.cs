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
    
    public Ingrediente whatTheyWhant;
    
    public Action<GameObject> PlayerisSit;
    public Action<pnjMove> ClientWhantTakeOrder;
    public Action<pnjMove> ClientWhait;
    public Action<pnjMove> ClientLeave;
    
    private float delayTime;
    private bool isCommanding = false;
    private bool hasArrived;
    private bool takeTheOrder = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    private void Start()
    {
        delayTime = whaitingTime;
    }
    private void Update()
    {
        switch (logic)
        {
            case cycle.Waiting:
                Waiting();
                break;
            case cycle.Command:
                Command();
                break;
            case cycle.Timer:
                Timer();
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
            agent.SetDestination(t.position);
        }
        
        if(target.transform != place.outSide.transform && 
           agent.remainingDistance < agent.stoppingDistance &&
           agent.velocity.magnitude > 0.5f)
        {
            PlayerisSit?.Invoke(player);
            logic = cycle.Command;
        }
    }
    private void Command()
    {
        if (!takeTheOrder)
        {
            ClientWhantTakeOrder?.Invoke(this);
            menu.StartTakeOrder(this);
        }
    }
    private void Timer()
    {
        delayTime -=Time.deltaTime;
        ClientWhait?.Invoke(this);
        if (delayTime <= 0)
        {
            logic = cycle.Exit;
        }
    }
    private void Exit()
    {
        Transform t = place.Leave(player);
        target = t;
        agent.SetDestination(target.position);
        hasArrived = false;
        ClientLeave?.Invoke(this);
    }

    public void Interact()
    {
        takeTheOrder =  true;

        if (logic == cycle.Timer)
        {
            foreach (var ingrediente in playerInventory.ingredientes)
            {
                if (ingrediente == whatTheyWhant)
                {
                    playerInventory.RemoveIngrediente(ingrediente);
                    moveAtTheList.Remove(ingrediente);
                    logic = cycle.Exit;
                }
            }
        }
    }

    public void EndInteraction()
    {
    }
}
