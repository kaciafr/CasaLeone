using UnityEngine;

namespace CharacterController
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody _rb;
        private PlayerMovement _playerMovement;
        [SerializeField] private InputManager _input; 

    
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
            _playerMovement = GetComponent<PlayerMovement>();
        }
    
        private void Update()
        {
            float speed = Mathf.Abs(_rb.linearVelocity.x);
            if (speed < 0.5f) speed = 0f; 
            _animator.SetFloat("Speed", speed);
            _animator.SetBool("IsGrounded", _playerMovement.IsGroundedState);
            _animator.SetBool("IsRunning", _input.IsRunning);
        }
    }
}