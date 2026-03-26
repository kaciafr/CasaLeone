using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class pnjMove : MonoBehaviour
{
    public enum cycle
    {
        Waiting,
        Command,
        Order,
        Check,
        Exit
    }
    public cycle logic;
    
    [Header("References")]
    
    public Transform target;
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private PhaseSysteme nextPhase;
    [SerializeField] private AllPlace place;
    [Header("Settings")]
    [SerializeField] private float updateSpeed = 0.1f;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;

    public Action<GameObject> PlayerisSit;
    
    private float time;
    private bool isCommanding = false;
    private bool hasArrived = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    private void Start()
    {
        
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
            /*case cycle.Order:
                Order();
                break;
            case cycle.Check:
                EatAndLeave();
                break;
            case cycle.Exit:
                Exit();
                break;*/
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
        
    }
}
