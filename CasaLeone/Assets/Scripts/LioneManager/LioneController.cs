using System;
using DefaultNamespace;
using LioneManager.State;
using Unity.VisualScripting;
using UnityEngine;

public class LioneController : MonoBehaviour
{
    public event Action<ILioneState> LioneStateChanged; 
    
    public LioneMovement LioneMovement;
    
    public ILioneState CurrentLioneState;
    public Transform waitPoint;

    [SerializeField] private float patrolRadius;
    [SerializeField] private float waitTimeAtPoint;

    
    
    private void Awake()
    {
        CurrentLioneState =  new PatrolState();
        SwitchLioneState(new PatrolState(patrolRadius,waitTimeAtPoint));

    } 
 

    void Update()
    {
        CurrentLioneState?.Update(this , Time.deltaTime);
    }

    public void SwitchLioneState(ILioneState newLioneState)
    {
        CurrentLioneState ?.Exit(this );
        CurrentLioneState = newLioneState;
        CurrentLioneState?.Enter(this );
        LioneStateChanged?.Invoke(CurrentLioneState);
    }
    
    
}
