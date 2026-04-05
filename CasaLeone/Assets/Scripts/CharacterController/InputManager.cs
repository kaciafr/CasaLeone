using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 Movement { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsInteracting { get; private set; }
    public bool IsDroppingThisFrame { get; private set; }
    


    private InputAction _moveAction;
    private InputAction _runAction;
    private InputAction _interactAction;
    private InputAction _dropAction;


    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        _moveAction     = playerInput.actions["Move"];
        _runAction      = playerInput.actions["Run"];
        _interactAction = playerInput.actions["Interact"];
        _dropAction     = playerInput.actions["Drop"];

    }

    private void Update()
    {
        Movement            = _moveAction.ReadValue<Vector2>();
        IsRunning           = _runAction.IsPressed();
        IsInteracting       = _interactAction.IsPressed();
        IsDroppingThisFrame = _dropAction.WasPressedThisFrame();
        
    }
}