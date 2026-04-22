using UnityEngine;

namespace LioneManager.State
{
    public class WaitPositionState : ILioneState
    {
        public Transform _targetPoint;

        public WaitPositionState(Transform targetPoint)
        {
            _targetPoint = targetPoint;
        }
        public void Enter(LioneController lioneController)
        {
            lioneController.LioneMovement.SetDestination(_targetPoint.position);

        }

        public void Exit(LioneController lioneController)
        {
        }

        public void Update(LioneController lioneController, float deltaTime)
        {
        }
    }
}