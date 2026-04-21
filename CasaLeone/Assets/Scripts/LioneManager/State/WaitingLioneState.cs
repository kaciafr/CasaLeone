using UnityEngine;

namespace LioneManager.State
{
    public class WaitingLioneState : ILioneState
    {
        public Transform waitPoint;

        public void Enter(LioneController lioneController)
        {
            lioneController.LioneMovement.SetDestination(lioneController.waitPoint.position);
        }

        public void Exit(LioneController lioneController)
        {
            
        }

        public void Update(LioneController lioneController)
        {
            
        }
    }
}