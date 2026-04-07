using UnityEngine;

namespace CharacterController
{
    [CreateAssetMenu( menuName = "PlayerMovement ")]
    public class MovementState : ScriptableObject
    {
        [Range(1f,100f)] public float MaxWalkSpeed = 12.5f ;
        [Range(0.25f, 50f)] public float GroundAcceleration = 5f;
        [Range(0.25f,50f)] public float  GroundDeceleration = 20f;
        [Range(0.25f,50f)] public float AirAcceleration =5f;
        [Range(0.25f,50f)] public float AirDeceleration = 5f;
        
        [Range(0.25f,50f)] public float MaxRunSpeed = 20f;
        
        public  LayerMask GroundLayer;
        public float GroundDetectionRayLength = 0.5f;
        


        public bool DebugShowIsGrounded { get; set; }

        
        public float GravityValue = -30f;   
        public float MaxFallSpeed = -20f;   

    }
}
