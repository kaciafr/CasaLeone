using System;
using System.Collections;
using CharacterController;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementState movementState;

    [SerializeField] private Collider feetCol;
    [SerializeField] private Collider bodyCol;
    private Rigidbody rb;

    public InputManager _input;

    private Vector3 _moveVelocity;
    private bool _isFacingRight = true;

    private RaycastHit _groundHit;
    private bool _isGrounded;
    public bool IsGroundedState => _isGrounded;

    private bool _isDropping = false;
    private Collider _currentPlatformCollider;
    private Collider _lastKnownPlatformCollider;
    public float multiplier = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Turn(true);
    }



    private void Update()
    {

        if (_input.IsDroppingThisFrame && _lastKnownPlatformCollider != null)
        {
            StartCoroutine(DropThroughPlatform());
        }
    }

    private void FixedUpdate()
    {
        CollisionCheck();
        Gravity();

        if (_isGrounded)
            Move(movementState.GroundAcceleration, movementState.GroundDeceleration, _input.Movement);
        else
            Move(movementState.AirAcceleration, movementState.AirDeceleration, _input.Movement);
    }

    #region Movement

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            TurnCheck(moveInput);

            Vector3 targetVelocity = new Vector3(moveInput.x, 0f, 0f) *
                                     (_input.IsRunning ? movementState.MaxRunSpeed : movementState.MaxWalkSpeed * multiplier);

            _moveVelocity = Vector3.Lerp(_moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            _moveVelocity = Vector3.Lerp(_moveVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        rb.linearVelocity = new Vector3(_moveVelocity.x, rb.linearVelocity.y, 0f); 
    }

    public void TurnCheck(Vector2 moveInput)
    {
        if (_isFacingRight && moveInput.x < 0)
            Turn(false);
        else if (!_isFacingRight && moveInput.x > 0)
            Turn(true);
    }

    public void Turn(bool turnRight)
    {
        _isFacingRight = turnRight;
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

        Vector3 boxCastOrigin = new Vector3(feetCol.bounds.center.x, feetCol.bounds.min.y, feetCol.bounds.center.z);
        Vector3 boxCastSize   = new Vector3(feetCol.bounds.size.x, movementState.GroundDetectionRayLength, feetCol.bounds.size.z);

        _isGrounded = Physics.BoxCast(
            boxCastOrigin,
            boxCastSize * 0.5f,            
            Vector3.down,
            out _groundHit,
            Quaternion.identity,
            movementState.GroundDetectionRayLength,
            movementState.GroundLayer
        );

        _currentPlatformCollider = null;

        RaycastHit[] hits = Physics.BoxCastAll(
            boxCastOrigin,
            boxCastSize * 0.5f,
            Vector3.down,
            Quaternion.identity,
            movementState.GroundDetectionRayLength
        );

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                _currentPlatformCollider = hit.collider;
                _lastKnownPlatformCollider = hit.collider;
                _isGrounded = true;
                break;
            }
        }

    }

    private void Gravity()
    {
        if (_isGrounded && !_isDropping)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); 
            return;
        }

        float newY = Mathf.Max(
            rb.linearVelocity.y + movementState.GravityValue * Time.fixedDeltaTime,
            movementState.MaxFallSpeed
        );

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, newY, rb.linearVelocity.z);
    }

    #endregion

    #region Drop Through Platform

    private IEnumerator DropThroughPlatform()
    {

        if (_lastKnownPlatformCollider == null)
        {
            yield break;
        }

        Collider platformToIgnore = _lastKnownPlatformCollider;
        _isDropping = true;


        Physics.IgnoreCollision(bodyCol, platformToIgnore, true);
        Physics.IgnoreCollision(feetCol, platformToIgnore, true);

        yield return new WaitForSeconds(0.5f);

        Physics.IgnoreCollision(bodyCol, platformToIgnore, false);
        Physics.IgnoreCollision(feetCol, platformToIgnore, false);

        _isDropping = false;

    }

    #endregion
}
