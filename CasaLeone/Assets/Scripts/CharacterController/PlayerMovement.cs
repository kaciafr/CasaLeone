using System.Collections;
using DG.Tweening;
using UnityEngine;


    public class PlayerMovement : MonoBehaviour
    {
        
        public MovementState movementState;
        [SerializeField] private Collider2D feetCol;
        [SerializeField] private Collider2D bodyCol;
        private Rigidbody2D rb2D;
        [SerializeField] private InputManager _input;
        private bool isClimbingStairs = false;
        public bool CanClimb { get; set; } = false;

        public Transform[] points;
        public float  duration; 



        private Vector2 moveVelocity;
        private bool isFacingRight = true;

        private RaycastHit2D groundHit;
        private bool isGrounded;
        public bool IsGroundedState => isGrounded;

        
        private bool isDropping = false;
        private Collider2D currentPlatformCollider; 

        private float stepHeight = 0.35f;   
        private float stepCheckDist = 0.15f; 
        

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            rb2D.gravityScale = 0f;
            Turn(true);
        }

        private void Update()
        {
            if (_input.IsDroppingThisFrame  && currentPlatformCollider != null)
                StartCoroutine(DropThroughPlatform());
        }

        private void FixedUpdate()
        {
            CollisionCheck();
            Gravity();

            if (isGrounded)
                Move(movementState.GroundAcceleration, movementState.GroundDeceleration, _input.Movement);
            else
                Move(movementState.AirAcceleration, movementState.AirDeceleration, _input.Movement);
            
            if (_input.IsInteracting && CanClimb) SlideStairs();
            //ClimbUp();

        }

        #region Movement

        private void Move(float acceleration, float deceleration, Vector2 moveInput)
        {
            if (moveInput != Vector2.zero)
            {
                TurnCheck(moveInput);

                Vector2 targetVelocity = new Vector2(moveInput.x, 0f) *
                    (_input.IsRunning ? movementState.MaxRunSpeed : movementState.MaxWalkSpeed);

                moveVelocity = Vector2.Lerp(moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            }
            else
            {
                moveVelocity = Vector2.Lerp(moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            }

            rb2D.linearVelocity = new Vector2(moveVelocity.x, rb2D.linearVelocity.y);
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
            if (isDropping)
            {
                isGrounded = false;
                currentPlatformCollider = null;
                return;
            }

            Vector2 boxCastOrigin = new Vector2(feetCol.bounds.center.x, feetCol.bounds.min.y);
            Vector2 boxCastSize   = new Vector2(feetCol.bounds.size.x, movementState.GroundDetectionRayLength);

           
            groundHit  = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down,
                movementState.GroundDetectionRayLength, movementState.GroundLayer);
            isGrounded = groundHit.collider != null;

           
            currentPlatformCollider = null;
            RaycastHit2D[] hits = Physics2D.BoxCastAll(
                boxCastOrigin, boxCastSize, 0f, Vector2.down, movementState.GroundDetectionRayLength);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Platform"))
                {
                    currentPlatformCollider = hit.collider;
                    isGrounded = true; 
                    break;
                }
            }
        }

        private void Gravity()
        {
            if (isGrounded && !isDropping) 
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, 0f);
                return;
            }

            float newY = Mathf.Max(
                rb2D.linearVelocity.y + movementState.GravityValue * Time.fixedDeltaTime,
                movementState.MaxFallSpeed
            );

            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, newY);
        }

        #endregion

        #region Drop Through Platform

        private IEnumerator DropThroughPlatform()
        {
            isDropping = true;
            
            bodyCol.enabled = false;
            feetCol.enabled = false;
            
            yield return new WaitForSeconds(0.4f);

            bodyCol.enabled = true;
            feetCol.enabled = true;
            
            isDropping = false;
        }

        #endregion

        /*private void ClimbUp()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                feetCol.bounds.center,
                Vector2.up,
                10f,                        
                movementState.GroundLayer
            );

            if (hit.collider == null) return;
            float targetY = hit.collider.bounds.max.y + feetCol.bounds.extents.y + 0.5f;
            transform.position = new Vector2(transform.position.x, targetY);

            rb2D.linearVelocity = Vector2.zero; 
        }*/

        private void SlideStairs()
        {
            Vector3[] waypoints = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++)
                waypoints[i] = points[i].position;

            Sequence seq = DOTween.Sequence();
            
            

            seq.Append(transform.DORotate(new Vector3(0f, 0f, 45f), 0.3f)
                .SetEase(Ease.OutSine));

            seq.Append(transform.DOPath(waypoints, duration, PathType.CatmullRom)
                .SetEase(Ease.InOutSine)
                .SetOptions(false));

            seq.Append(transform.DORotate(new Vector3(0f, 0f, 0f), 0.3f)
                .SetEase(Ease.InSine));
        }
        
        
     

        
    }