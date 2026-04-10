using UnityEngine;

namespace Players.CharacterController
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rb2D;
        private PlayerMovement _playerMovement;
        [SerializeField] private InputManager _input; 

    
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb2D = GetComponent<Rigidbody2D>();
            _playerMovement = GetComponent<PlayerMovement>();
        }
    
        private void Update()
        {
            float speed = Mathf.Abs(_rb2D.linearVelocity.x);
            if (speed < 0.5f) speed = 0f; 
            _animator.SetFloat("Speed", speed);
            _animator.SetBool("IsGrounded", _playerMovement.IsGroundedState);
            _animator.SetBool("IsRunning", _input.IsRunning);
        }
    }
}