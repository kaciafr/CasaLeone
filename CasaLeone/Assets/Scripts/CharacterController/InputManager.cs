using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput playerInput;
    public static Vector2 Movement;
    public static bool isRunning; 
    public static bool isInteracting;

 
    private InputAction _moveAction;
    private InputAction _runAction;
    private InputAction _interactAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        
        _moveAction = playerInput.actions["Move"];
        _runAction = playerInput.actions["Run"];
        _interactAction = playerInput.actions["Interact"];
        
        
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        isRunning = _runAction.IsPressed();
        isInteracting = _interactAction.IsPressed();
        
      
    }
}
