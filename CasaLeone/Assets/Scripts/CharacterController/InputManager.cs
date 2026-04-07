using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterController
{
    public class InputManager : MonoBehaviour
    {
        public Vector2 Movement { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsInteracting { get; private set; }
        public bool IsDroppingThisFrame { get; private set; }
    
        public bool isActivateStairs { get; private set; }
        public bool isDeactivateStairs { get; private set; }


        private InputAction _moveAction;
        private InputAction _runAction;
        private InputAction _interactAction;
        private InputAction _dropAction;
        private InputAction _climbStairsActionActivate;
        private InputAction _climbStairsActionDeactivate;


        private void Awake()
        {
            var playerInput = GetComponent<PlayerInput>();
            _moveAction     = playerInput.actions["Move"];
            _runAction      = playerInput.actions["Run"];
            _interactAction = playerInput.actions["Interact"];
            _dropAction     = playerInput.actions["Drop"];
            _climbStairsActionActivate = playerInput.actions["StairsClimbActivate"];
            _climbStairsActionDeactivate = playerInput.actions["StairsClimbDeactivate"];

        }

        private void Update()
        {
            Movement            = _moveAction.ReadValue<Vector2>();
            IsRunning           = _runAction.IsPressed();
            IsInteracting       = _interactAction.IsPressed();
            IsDroppingThisFrame = _dropAction.WasPressedThisFrame();
            isActivateStairs    = _climbStairsActionActivate.IsPressed();
            isDeactivateStairs  = _climbStairsActionDeactivate.IsPressed();
        }
    }
}