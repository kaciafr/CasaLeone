
using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerMovement : MonoBehaviour
    {
        public MovementState movementState;
        [SerializeField] private Collider2D _feetCol;
        [SerializeField] private Collider2D _bodyCol;
        private Rigidbody2D _rb2D;

        private Vector2 _moveVelocity;
        private bool isFacingRight;


        private RaycastHit2D _groundhit;
        private RaycastHit2D _Headhit;
        private bool _isGrounded;
        private bool _bumpedHead;
        private bool isOnPlatform;
        
        private Collider2D _platformCollider;
        private bool _isDropping = false;
        
        
        
        
        private float coyoteTimer;
        

        private void Awake()
        {
            isFacingRight = true;
            _rb2D = GetComponent<Rigidbody2D>();
            _rb2D.gravityScale = 0f;
            
        }

        private void Update()
        {
            if (isOnPlatform && !_isDropping && _platformCollider != null && InputManager.Movement.y < -0.5f)
            {
                StartCoroutine(DropCoroutine());
            }
        }
        private IEnumerator DropCoroutine()
        {
            _isDropping = true;

            if (_platformCollider != null)
                _platformCollider.enabled = false;

            yield return new WaitForSeconds(0.5f);

            if (_platformCollider != null)
                _platformCollider.enabled = true;

            _isDropping = false;
        }

        private void FixedUpdate()
        {
            CollisionCheck();
            Gravity();

            if (_isGrounded)
            {
                Move(movementState.GroundAcceleration, movementState.GroundDeceleration, InputManager.Movement);
            }
            else
            {
                Move(movementState.AirAcceleration, movementState.AirDeceleration, InputManager.Movement);
            }
        }

     

        #region Movement

        private void Move(float acceleration, float deceleration, Vector2 moveInput)
        {
            if (moveInput != Vector2.zero)
            {
                TurnCheck(moveInput);
                Vector2 targetVelocity = Vector2.zero;
                if (InputManager.isRunning)
                {
                    targetVelocity = new Vector2(moveInput.x, 0f) * movementState.MaxRunSpeed;
                }
                else
                {
                    targetVelocity = new Vector2(moveInput.x, 0f) * movementState.MaxWalkSpeed;
                }
                _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, acceleration * Time.deltaTime);
                _rb2D.linearVelocity = new Vector2(_moveVelocity.x, _rb2D.linearVelocity.y);
            }
            else
            {
                _moveVelocity = Vector2.Lerp(_moveVelocity, Vector2.zero, deceleration * Time.deltaTime);
                _rb2D.linearVelocity = new Vector2(_moveVelocity.x, _rb2D.linearVelocity.y);
            }
        }

        #endregion
        
        private void TurnCheck(Vector2 moveInput)
        {
            if (isFacingRight && moveInput.x < 0)
            {
                Turn(false);
            }
            else if (!isFacingRight && moveInput.x > 0)
            {
                Turn(true);
            }
        }

        private void Turn(bool turnRight)
        {
            isFacingRight = turnRight;
            Vector3 scale = transform.localScale;
            scale.x = turnRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        private void IsGrounded()
        {
            Vector2 boxCastOrigin = new Vector2(_feetCol.bounds.center.x, _feetCol.bounds.min.y);
            Vector2 boxCastSize = new Vector2(_feetCol.bounds.size.x, movementState.GroundDetectionRayLength);

            _groundhit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down,
                movementState.GroundDetectionRayLength, movementState.GroundLayer);

            _isGrounded = _groundhit.collider != null;

            if (_groundhit.collider != null && _groundhit.collider.CompareTag("Platform"))
                isOnPlatform = true;
            else if (!_isDropping)
                isOnPlatform = false;
        }

        private void CollisionCheck()
        {
            if (_isDropping)
            {
                _isGrounded = false; 
                return;
            }
            IsGrounded();
        }

        private void Gravity()
        {
            if (_isGrounded && !_isDropping) 
            {
                _rb2D.linearVelocity = new Vector2(_rb2D.linearVelocity.x, 0f);
                return;
            }

            float newY = Mathf.Max(
                _rb2D.linearVelocity.y + movementState.GravityValue * Time.fixedDeltaTime,
                movementState.MaxFallSpeed
            );

            _rb2D.linearVelocity = new Vector2(_rb2D.linearVelocity.x, newY);
        }

        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                isOnPlatform = true;
                _platformCollider = other.collider;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                isOnPlatform = false;
            }
        }

        
      
        
        
      
    }
}