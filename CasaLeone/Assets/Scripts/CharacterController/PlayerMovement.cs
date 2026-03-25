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
        private bool isFacingRight = true;

        private RaycastHit2D _groundHit;
        private bool _isGrounded;
        public bool IsGroundedState => _isGrounded;

        private bool _isDropping = false;
        private Collider2D _currentPlatformCollider; 

        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _rb2D.gravityScale = 0f;
        }

        private void Update()
        {
            if (InputManager.isDroppingThisFrame && _currentPlatformCollider != null)
                StartCoroutine(DropThroughPlatform());
        }

        private void FixedUpdate()
        {
            CollisionCheck();
            Gravity();

            if (_isGrounded)
                Move(movementState.GroundAcceleration, movementState.GroundDeceleration, InputManager.Movement);
            else
                Move(movementState.AirAcceleration, movementState.AirDeceleration, InputManager.Movement);
        }

        #region Movement

        private void Move(float acceleration, float deceleration, Vector2 moveInput)
        {
            if (moveInput != Vector2.zero)
            {
                TurnCheck(moveInput);

                Vector2 targetVelocity = new Vector2(moveInput.x, 0f) *
                    (InputManager.isRunning ? movementState.MaxRunSpeed : movementState.MaxWalkSpeed);

                _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            }
            else
            {
                _moveVelocity = Vector2.Lerp(_moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            }

            _rb2D.linearVelocity = new Vector2(_moveVelocity.x, _rb2D.linearVelocity.y);
        }

        private void TurnCheck(Vector2 moveInput)
        {
            if (isFacingRight && moveInput.x < 0)
                Turn(false);
            else if (!isFacingRight && moveInput.x > 0)
                Turn(true);
        }

        private void Turn(bool turnRight)
        {
            isFacingRight = turnRight;
            Vector3 scale = transform.localScale;
            scale.x = turnRight ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        #endregion

        #region Collision & Gravity

        private void CollisionCheck()
        {
            if (_isDropping)
            {
                _isGrounded = false;
                _currentPlatformCollider = null;
                return;
            }

            Vector2 boxCastOrigin = new Vector2(_feetCol.bounds.center.x, _feetCol.bounds.min.y);
            Vector2 boxCastSize   = new Vector2(_feetCol.bounds.size.x, movementState.GroundDetectionRayLength);

           
            _groundHit  = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down,
                movementState.GroundDetectionRayLength, movementState.GroundLayer);
            _isGrounded = _groundHit.collider != null;

           
            _currentPlatformCollider = null;
            RaycastHit2D[] hits = Physics2D.BoxCastAll(
                boxCastOrigin, boxCastSize, 0f, Vector2.down, movementState.GroundDetectionRayLength);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Platform"))
                {
                    _currentPlatformCollider = hit.collider;
                    _isGrounded = true; 
                    break;
                }
            }
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

        #endregion

        #region Drop Through Platform

        private IEnumerator DropThroughPlatform()
        {
            _isDropping = true;

            Collider2D platformCollider = _currentPlatformCollider;
            platformCollider.enabled = false;

            yield return new WaitForSeconds(0.4f);

            platformCollider.enabled = true;
            _isDropping = false;
        }

        #endregion
    }
}
