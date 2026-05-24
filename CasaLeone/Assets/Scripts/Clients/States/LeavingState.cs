using System;
using Restaurants;
using UnityEngine;

namespace Clients.States
{
    public class LeavingState : IClientState
    {
       public readonly bool IsAngry;
       private bool isReady = true;
       public event Action ReplicaLine;

       public LeavingState(bool isAngry)
       {
          IsAngry = isAngry;
       }

       public void Enter(ClientController controller)
       {
          isReady = true;

          if (controller.CurrentSeat != null)
          {
             controller.CurrentSeat.Leave(controller); 
             
             if (controller.CurrentSeat.table != null)
             {
                controller.CurrentSeat.table.CheckIfTableIsNowEmpty();
             }
             
             controller.CurrentSeat = null; 
          }

          QueueManager.Instance.LeaveTheQueue(controller);

          if (IsAngry)
          {
             Restaurant.Instance.AddOrRemoveStress(8);
             RoundEcran.Instance.SadScore();
          }
          else
          {
             Restaurant.Instance.AddOrRemoveStress(-3);
             RoundEcran.Instance.HappyScore();
          }

       }

       public void Exit(ClientController controller)
       {
       }

       public void Update(ClientController controller, float deltaTime)
       {
          controller.Movement.SetDestination(Restaurant.Instance.Exit);
          
          if (isReady)
          {
             ReplicaLine?.Invoke();
             isReady = false;
          }
          
          if (controller.Movement.HasArrived())
          {
             controller.Despawn();
          }
       }
    }
}