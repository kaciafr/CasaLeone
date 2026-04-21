using UnityEngine;

namespace LioneManager.State
{
    public class PatrolLioneState : ILioneState
    {
        private float _patrolRadius;      
        private float _waitTimeAtPoint;   
        private float _timer;
        private bool _isWaiting;

        public PatrolLioneState(float patrolRadius = 10f, float waitTimeAtPoint = 2f)
        {
            _patrolRadius    = patrolRadius;
            _waitTimeAtPoint = waitTimeAtPoint;
        }

        public void Enter(LioneController lioneController)
        {
            GoToRandomPoint(lioneController);
        }

        public void Update(LioneController lioneController)
        {
            if (lioneController.LioneMovement.HasReachedDestination())
            {
                if (!_isWaiting)
                {
                    _isWaiting = true;
                    _timer     = 0f;
                }

                _timer += Time.deltaTime;

                if (_timer >= _waitTimeAtPoint)
                {
                    _isWaiting = false;
                    GoToRandomPoint(lioneController);
                }
            }
        }

        public void Exit(LioneController lioneController)
        {
            _isWaiting = false;
            _timer     = 0f;
        }

        private void GoToRandomPoint(LioneController lioneController)
        {
            Vector3 randomPoint = GetRandomNavMeshPoint(
                lioneController.transform.position,
                _patrolRadius
            );

            lioneController.LioneMovement.SetDestination(randomPoint);
        }

        private Vector3 GetRandomNavMeshPoint(Vector3 origin, float radius)
        {
            Vector2 randomCircle = Random.insideUnitCircle * radius;
            Vector3 randomPoint  = origin + new Vector3(randomCircle.x, 0f, randomCircle.y);

            if (UnityEngine.AI.NavMesh.SamplePosition(
                    randomPoint, 
                    out UnityEngine.AI.NavMeshHit hit, 
                    radius, 
                    UnityEngine.AI.NavMesh.AllAreas))
            {
                return hit.position;
            }
            return origin;
        }
    }
}
