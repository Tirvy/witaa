using System;
using UnityEngine;

namespace PixelCrown
{
    // Configuration Activation structure
    [Serializable]
    public class ConfigurationActivation
    {
        [SerializeField]
        [Tooltip("Enable jump")]
        public bool enableJump = true;

        [SerializeField]
        [Tooltip("Enable walk")]
        public bool enableWalk = true;

        [SerializeField]
        [Tooltip("Enable always run")]
        public bool enableAlwaysRun = true;

        [SerializeField]
        [Tooltip("Enable long jump. character can jump higher by keep pressing the jump button")]
        public bool enableLongJump = true;

        [SerializeField]
        [Tooltip("Enable character double jump")]
        public bool enableDoubleJump = true;

        [SerializeField]
        [Tooltip("Enable wall surfing. character can slow down fall wall by going forward a wall")]
        public bool enableWallSurfing = true;

        [SerializeField]
        [Tooltip("Enable wall jump. character can jump again if against a wall")]
        public bool enableWallJump = true;

        [SerializeField]
        [Tooltip("Enable character crouch")]
        public bool enableCrouch = true;

        [SerializeField]
        [Tooltip("Enable changing direction during jump")]
        public bool enableAirControl = true;

        [SerializeField]
        [Tooltip("Enable to keep momentum if pushed in the air by other objects")]
        public bool enableAirPush = false;
    }

    // Movement structure
    [Serializable]
    public class ConfigurationMovement
    {
        [Tooltip("Run speed")]
        public float runSpeed = 600.0f;

        [Tooltip("Walk speed")]
        public float walkSpeed = 300.0f;

        [Tooltip("Force up for the initial jump")]
        public float jumpForce = 500.0f;

        [Tooltip("Force up for the long jump")]
        public float longJumpForce = 1200.0f;

        [Tooltip("Time for the long jump in seconds")]
        public float longJumpTime = 0.6f;

        [Tooltip("Force away from wall to apply to character when wall jumping")]
        public float wallJumpPush = 400f;

        [Range(0, 1.0f)]
        [Tooltip("Max jump factor relative to jump force when wall jumping. 1 = 100%")]
        public float wallJump = 0.8f;

        [Range(0, 1.0f)]
        [Tooltip("The character have an extra time to jump when falling ledge or wall jumping")]
        public float justInTimeJump = 0.2f;

        [Tooltip("Maximum number of double jump")]
        public int doubleJumpMax = 1;

        [Range(0, 1)]
        [Tooltip("Max speed factor applied to crouching movement relative to walking speed. 1 = 100%")]
        public float crouchWalkSpeed = 0.5f;

        [Range(0, 1.0f)]
        [Tooltip("Smooth of the character movement when on the ground")]
        public float groundMovementSmoothing = 0.15f;

        [Range(0, 1.0f)]
        [Tooltip("Smooth of the character movement when in the air")]
        public float airMovementSmoothing = 0.15f;

        [Range(0, 1.0f)]
        [Tooltip("Friction on the wall when wall surfing")]
        public float wallSurfingFriction = 0.5f;

        [Range(0, 2.0f)]
        [Tooltip("Air direction control when changing direction")]
        public float airMovement = 0.5f;

        [Range(0, 4.0f)]
        [Tooltip("Change falling gravity when falling. Normal gravity is 1")]
        public float jumpingGravity = 2.0f;

        [Range(0, 4.0f)]
        [Tooltip("Change falling gravity when falling. Normal gravity is 1")]
        public float fallingGravity = 2.75f;

        [Range(0, 2.0f)]
        [Tooltip("Friction factor of the character on the ground")]
        public float groundFriction = 1.0f;

        [Range(0, 2.0f)]
        [Tooltip("Friction factor of the character in the air")]
        public float airFriction = 0.2f;

        [Tooltip("List of animator components that handles the character animation. Optional")]
        public Animator[] animators;
    }


    // Configuration Detection structure
    [Serializable]
    public class ConfigurationDetection
    {
        [SerializeField]
        [Tooltip("Which layer is ground for ground and wall detection. optional")]
        public LayerMask whatIsGround;

        [SerializeField]
        [Tooltip("Object to detect ground to be placed at the bottom of the object sprite. optional")]
        public Transform groundCheckObject;

        [SerializeField]
        [Tooltip("Object to detect ceiling to be placed at the top of the object sprite. optional")]
        public Transform ceilingCheckObject;

        [SerializeField]
        [Tooltip("Object to spawn step effects and landing effects. optional")]
        public Transform stepCheckObject;

        [SerializeField]
        [Tooltip("Object to detect wall to be placed in front of character sprite. optional")]
        public Transform frontWallCheckObject;

        [SerializeField]
        [Tooltip("Collider used when crouching. optional")]
        public Collider2D crouchCollider;

        [SerializeField]
        [Tooltip("Collider used when standing. optional")]
        public Collider2D standCollider;

        [SerializeField]
        [Tooltip("Detection radius for ground and wall detection")]
        public float detectRadius = 0.1f;

        private float groundDetectionTime = -10f;
        private float ceilingDetectionTime = -10f;
        private float fronWallDetectionTime = -10f;
        private float detectionDuration = 0.15f;

        public void OnGroundDetected()
        {
            groundDetectionTime = Time.time;
        }

        public void OnCeilingDetected()
        {
            ceilingDetectionTime = Time.time;
        }

        public void OnFrontWallDetected()
        {
            fronWallDetectionTime = Time.time;
        }

        // Drawing sphere Gizmos for the detection
        public void OnDrawGizmos()
        {

            // Draw ground detection
            if (groundCheckObject)
            {
                DrawGizmo(groundCheckObject.position, detectRadius, groundDetectionTime);
            }

            // Draw ceiling detection
            if (ceilingCheckObject)
            {
                DrawGizmo(ceilingCheckObject.position, detectRadius, ceilingDetectionTime);
            }

            // Draw wall detection
            if (frontWallCheckObject)
            {
                DrawGizmo(frontWallCheckObject.position, detectRadius, fronWallDetectionTime);
            }
        }

        private void DrawGizmo(Vector3 position, float radius, float detectionTime)
        {
            if (Time.time - detectionTime < detectionDuration)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.blue;
            }
            Gizmos.DrawWireSphere(position, detectRadius);

            Gizmos.color = new Color(0f, 0.1f, 1f, 0.25f);
            Gizmos.DrawSphere(position, detectRadius);

            if (Time.time - detectionTime < detectionDuration)
            {
                float factor = 1 - ((Time.time - detectionTime) / detectionDuration);
                Gizmos.color = new Color(1f, 0f, 0f, factor);
            }
            Gizmos.DrawSphere(position, detectRadius);
        }
    }


    // Configuration Effect structure
    [Serializable]
    public class ConfigurationEffect
    {
        [SerializeField]
        [Tooltip("Emitted effect in stepCheckObject position when walking. optional")]
        public GameObject dustWalkingEmitter;

        [SerializeField]
        [Tooltip("Emitted effect in stepCheckObject position when jumping. optional")]
        public GameObject dustJumpingEmitter;

        [Tooltip("Emitted effect in stepCheckObject when landing on ground. optional")]
        public GameObject dustLandingEmitter;

        [Tooltip("Emitted effect in frontWallCheckObject position when landing on ground. optional")]
        public GameObject dustWallSurfingEmitter;

        [Tooltip("Emitted effect in stepCheckObject position when double jumping. optional")]
        public GameObject dustDoubleJumpEmitter;

        [Tooltip("Time between effects when walking in seconds")]
        public float dustTime = 0.2f;
    }


    public class CharacterMovement2D : MonoBehaviour
    {
        [SerializeField]
        public PixelCrown.ConfigurationMovement m_movement;

        [SerializeField]
        public PixelCrown.ConfigurationActivation m_activation;

        [SerializeField]
        public PixelCrown.ConfigurationDetection m_detection;

        [SerializeField]
        public PixelCrown.ConfigurationEffect m_effect;

        public bool m_startFacingRight = true;

        // True if the character is crouching
        protected bool m_isCrouching = false;

        // True if the character is not on ground and going up
        protected bool m_isJumping = false;

        // True if the character is not on ground and going up
        protected bool m_isWalking = false;

        // True if the character is not on ground and going up
        protected bool m_isRunning = false;

        // True if the character is not on the ground and going down
        protected bool m_isFalling = false;

        // True if the character is wall surfing
        protected bool m_isWallSurfing = false;


        // For determining which way the character is currently facing. Use this from the animator to check for
        // the character direction
        protected bool m_isFacingRight = true;

        // True if character is on ground
        protected bool m_isGrounded = false;

        // Reference to velocity
        protected Vector3 m_velocity = Vector3.zero;

        // Last known position
        protected Vector3 m_lastPosition;

        // True if front wall is detected
        protected bool m_frontWallDetected = false;

        // Maximum time to end long jump while jumping 
        private float m_longJumpEndTime = 0.0f;

        // Check if the jump button was released
        private bool m_releaseJump = true;

        // Time when the jump button was released
        private float m_releaseJumpTime = 0.0f;

        // Number of double jump executed
        private int m_doubleJumpCount = 0;

        // Time when the jump started
        private float m_jumpStartTime = 0.0f;

        // Last time dust effect was spawned
        private float m_dustEndTime = 0.0f;

        // If the character can currently double jump
        private bool m_canDoubleJump = false;

        // If the character can currently wall jump
        private bool m_canWallJump = false;

        // Last valid wall jump direction
        private bool m_wallJumpRight = false;

        // Local rigid body of character to apply movement
        protected Rigidbody2D m_localRigidbody2D;

        // Stop movement until time
        private float m_movementDisableTime = 0f;

        // Slow horizon just after wall jumping
        private float m_wallJumpSlowDuration = 0.5f;

        // Slow horizontal until 
        private float m_wallJumpEndTime = 0f;

        // 
        private int m_lastFrameCount = 0;

        // The last time the character can jump
        private float m_canJumpTime = 0f;

        // The last time the character can jump
        private float m_canWallJumpTime = 0f;

        public void Start()
        {
            // Get the local Rigibody 2d
            m_localRigidbody2D = GetComponent<Rigidbody2D>();
            if (m_localRigidbody2D)
            {
                m_localRigidbody2D.sharedMaterial = new PhysicsMaterial2D();
                m_localRigidbody2D.sharedMaterial.bounciness = 0.0f;
                m_localRigidbody2D.sharedMaterial.friction = 0.0f;
            }

            // Get the animator if the array is not set
            if (m_movement == null || m_movement.animators == null || m_movement.animators.Length == 0)
            {
                Animator animator = GetComponent<Animator>();
                if (animator)
                {
                    m_movement.animators = new Animator[1];
                    m_movement.animators[0] = animator;
                }
            }

            // Last position to use with friction
            m_lastPosition = transform.position;

            if (!m_startFacingRight)
            {
                Flip();
            }
        }

        private void OnDrawGizmos()
        {
            if (m_detection != null)
            {
                m_detection.OnDrawGizmos();
            }
        }

        // Check for wall in front of character
        private void CheckWalls()
        {
            Collider2D[] colliders;

            // if the front wall check object is set
            if (m_detection.frontWallCheckObject)
            {
                m_frontWallDetected = false;

                // Get everything in the detect radius that is ground
                colliders = Physics2D.OverlapCircleAll(m_detection.frontWallCheckObject.position, m_detection.detectRadius, m_detection.whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    // unless it is the gameObject
                    if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger)
                    {
                        // front wall is detected
                        m_frontWallDetected = true;
                        m_detection.OnFrontWallDetected();

                        // stop here
                        break;
                    }
                }
            }
        }

        // Check for ground below character
        private void CheckGround()
        {
            // No ground check object
            if (!m_detection.groundCheckObject)
            {
                // Always on ground
                m_isGrounded = true;
                return;
            }

            // If going up
            if (!m_isGrounded && m_localRigidbody2D.velocity.y > 0.05f)
            {
                // Dont check ground
                m_isGrounded = false;
                return;
            }

            bool wasGrounded = m_isGrounded;
            m_isGrounded = false;

            // detects colliders below character
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_detection.groundCheckObject.position, m_detection.detectRadius, m_detection.whatIsGround);

        
            for (int i = 0; i < colliders.Length; i++)
            {
                // If collider is not gameObject
                if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger)
                {
                    m_isGrounded = true;
                    m_detection.OnGroundDetected();

                    // If was grounded and objects are set
                    if (m_effect.dustLandingEmitter && m_detection.stepCheckObject && !wasGrounded)
                    {
                        // spawn dust landing effect
                        Instantiate(m_effect.dustLandingEmitter, m_detection.stepCheckObject.position, m_detection.stepCheckObject.rotation);
                    }
                    break;
                }
            }
        }

        public void Flip()
        {
            // Switch object facing
            m_isFacingRight = !m_isFacingRight;

			transform.Rotate(0f, 180f, 0f);

            // Check wall a 2nd time because we
            CheckWalls();
        }

        public void MovementDisable(float duration)
        {
            m_movementDisableTime = Time.time + duration;
        }

        public void MovementEnable()
        {
            m_movementDisableTime = 0f;
        }

        // Move the character toward a direction or make it crouch / jump / walk
        public void Move(float direction, bool crouch, bool jump, bool walkToggle)
        {
            // Skip if already called for this frame
            if (m_lastFrameCount == Time.frameCount)
            {
                return;
            }
            m_lastFrameCount = Time.frameCount;
            Vector3 currentPosition = transform.position;

            if (m_movementDisableTime >= Time.time)
            {
                direction = 0f;
                crouch = false;
                jump = false;
                walkToggle = false;
            }

            bool running = false;
            bool walking = false;
            bool crouching = false;

            bool wasCrouching = m_isCrouching;
            bool wasWalking = m_isWalking;
            bool wasRunning = m_isRunning;
            bool wasWallSurfing = m_isWallSurfing;
            bool wasJumping = m_isJumping;
            bool wasFalling = m_isFalling;
            bool wasGrounded = m_isGrounded;


            if (!m_activation.enableAlwaysRun && walkToggle || m_activation.enableAlwaysRun && !walkToggle)
            {
                running = true;
                walking = false;
            }
            else
            {
                running = false;
                walking = true;
            }
            // Dont enable jump
            if (jump && !m_activation.enableJump)
            {
                jump = false;
            }

            // We can't crouch and jump at the same time
            if (crouch && jump)
            {
                crouch = false;
                crouching = false;
            }

            // Check the ground
            CheckGround();

            // Check the walls
            CheckWalls();

            // Check when the jump button is released
            if (m_activation.enableJump && !jump && !m_releaseJump && m_releaseJumpTime < Time.time + 0.1f)
            {
                m_releaseJump = true;
            }

            // Set the speed
            float speed = 0;
            if (running)
            {
                speed = direction * m_movement.runSpeed * Time.deltaTime * 100;
            }
            else
            {
                speed = direction * m_movement.walkSpeed * Time.deltaTime * 100;
            }

            // Check if character can stand
            if (m_detection.ceilingCheckObject && !crouch && m_isGrounded)
            {
                // Keep character crouching
                Collider2D[] colliders = Physics2D.OverlapCircleAll(m_detection.ceilingCheckObject.position, m_detection.detectRadius, m_detection.whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    // unless it is the gameObject
                    if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger)
                    {
                        crouch = true;
                        crouching = true;
                        m_detection.OnCeilingDetected();

                        // Also cannot jump when crouched
                        jump = false;
                    }
                }
            }

            // only control the character if grounded
            if (m_isGrounded)
            {
                // If crouching
                if (m_activation.enableCrouch && crouch)
                {
                    crouching = true;
                    walking = true;
                    running = false;
                    speed = direction * m_movement.walkSpeed * m_movement.crouchWalkSpeed * Time.deltaTime * 100;

                    // Change colliders when crouching
                    if (m_detection.crouchCollider != null)
                    {
                        m_detection.crouchCollider.isTrigger = false;
                    }
                    if (m_detection.standCollider != null)
                    {
                        m_detection.standCollider.isTrigger = true;
                    }
                }
                else
                {
                    crouching = false;

                    // Change colliders when crouching
                    if (m_detection.standCollider != null)
                    {
                        m_detection.standCollider.isTrigger = false;
                    }
                    if (m_detection.crouchCollider != null)
                    {
                        m_detection.crouchCollider.isTrigger = true;
                    }
                }

                if (speed != 0)
                {
                    // Move the character by finding the target velocity
                    Vector2 targetVelocity = new Vector2(speed * Time.deltaTime, m_localRigidbody2D.velocity.y);

                    // And then smoothing it out and applying it to the character
                    m_localRigidbody2D.velocity = Vector3.SmoothDamp(m_localRigidbody2D.velocity, targetVelocity, ref m_velocity, m_movement.groundMovementSmoothing);
                }

                // Simulated horizontal friction
                if (m_localRigidbody2D.velocity.x != 0)
                {
                    Vector2 friction = Vector2.zero;
                    friction.x = m_localRigidbody2D.velocity.x - m_localRigidbody2D.velocity.x * 10.0f * Time.deltaTime * m_movement.groundFriction;
                    friction.y = m_localRigidbody2D.velocity.y;

                    // Do not reverse velocity because of friction
                    if (m_localRigidbody2D.velocity.x > 0 && friction.x < 0 || m_localRigidbody2D.velocity.x > 0 && friction.x < 0)
                    {
                        friction.x = 0;
                    }
                    m_localRigidbody2D.velocity = friction;
                }

                // Set gravity
                m_localRigidbody2D.gravityScale = 1.0f;

                // If the input is moving the character right and the character is facing left...
                if (speed > 0 && !m_isFacingRight)
                {
                    // Flip the character
                    Flip();
                }
                // Otherwise if the input is moving the character left and the character is facing right...
                else if (speed < 0 && m_isFacingRight)
                {
                    // Flip the character
                    Flip();
                }
            }
            
            // Only controls character if it is in the air
            if (!m_isGrounded)
            {
                if (m_detection.crouchCollider)
                {
                    m_detection.crouchCollider.isTrigger = true;
                }
                if (m_detection.standCollider)
                {
                    m_detection.standCollider.isTrigger = false;
                }

                // only control the character if airControl is turned on
                if (m_activation.enableAirControl)
                {
                    if (speed != 0)
                    {
                        // slow air control just after a wall jump
                        if (Time.time < m_wallJumpEndTime)
                        {
                            float factor = (1 - ((m_wallJumpEndTime - Time.time) / m_wallJumpSlowDuration));
                            speed *= factor;
                        }

                        // Move the character
                        Vector3 targetVelocity = new Vector2(speed * Time.deltaTime * m_movement.airMovement, m_localRigidbody2D.velocity.y);

                        // And then smoothing it out and applying it to the character
                        m_localRigidbody2D.velocity = Vector3.SmoothDamp(m_localRigidbody2D.velocity, targetVelocity, ref m_velocity, m_movement.airMovementSmoothing);
                    }
                }

                // Simulated air friction
                if (m_localRigidbody2D.velocity.x != 0)
                {
                    Vector2 friction = Vector2.zero;
                    friction.x = m_localRigidbody2D.velocity.x - m_localRigidbody2D.velocity.x * 5.0f * Time.deltaTime * m_movement.airFriction;
                    friction.y = m_localRigidbody2D.velocity.y;

                    // Do not reverse velocity because of friction
                    if (m_localRigidbody2D.velocity.x > 0 && friction.x < 0 || m_localRigidbody2D.velocity.x > 0 && friction.x < 0)
                    {
                        friction.x = 0;
                    }
                    m_localRigidbody2D.velocity = friction;
                }

                // Simulated air push by other objects
                if (m_lastPosition != null && m_activation.enableAirPush)
                {
                    Vector2 targetVelocity = new Vector2(currentPosition.x - m_lastPosition.x, currentPosition.y - m_lastPosition.y);
                    m_localRigidbody2D.velocity = Vector3.SmoothDamp(m_localRigidbody2D.velocity, targetVelocity * 35f, ref m_velocity, 0.15f);
                }

                // Set falling gravity
                if (m_localRigidbody2D.velocity.y < 0)
                {
                    m_localRigidbody2D.gravityScale = m_movement.fallingGravity;
                }
                else
                {
                    m_localRigidbody2D.gravityScale = m_movement.jumpingGravity;
                }

                // If the input is moving the character right and the character is facing left
                if (speed > 0 && !m_isFacingRight && m_wallJumpEndTime < Time.time)
                {
                    // Flip the character
                    Flip();
                }
                // Otherwise if the input is moving the character left and the character is facing right
                else if (speed < 0 && m_isFacingRight && m_wallJumpEndTime < Time.time)
                {
                    // Flip the character
                    Flip();
                }
            }

            // Stops long jump if character is going down
            if (!m_isGrounded && Time.time > m_longJumpEndTime && m_localRigidbody2D.velocity.y <= 0)
            {
                m_longJumpEndTime = Time.time;
            }


            // Prevents multiple jumps if character is close to ground in multiple frames
            bool justJumped = false;
            if (Time.time - m_jumpStartTime < 0.2f)
            {
                justJumped = true;
            }

            // Just in time jump
            if (m_activation.enableJump && m_releaseJump && m_isGrounded && !justJumped)
            {
                m_canJumpTime = Time.time + m_movement.justInTimeJump;
            }

            // Just in time wall jump
            if (m_activation.enableJump && !m_isGrounded && m_activation.enableWallJump && m_canWallJump && !justJumped)
            {
                m_canWallJumpTime = Time.time + m_movement.justInTimeJump;
            }

            // Character jump
            if (m_activation.enableJump && m_releaseJump && m_isGrounded && !justJumped && jump
                || m_activation.enableJump && m_releaseJump && !m_isGrounded && !justJumped && jump && m_canJumpTime > Time.time)
            {
                // Add a vertical force to the character
                m_isGrounded = false;
                m_releaseJump = false;
                m_canDoubleJump = false;
                m_releaseJumpTime = Time.time;
                m_jumpStartTime = Time.time;
                m_longJumpEndTime = Time.time + m_movement.longJumpTime;
                m_doubleJumpCount = m_movement.doubleJumpMax;

                m_localRigidbody2D.AddForce(new Vector2(0f, m_movement.jumpForce));

                // add jump effect
                if (m_effect.dustJumpingEmitter && m_detection.stepCheckObject)
                {
                    Instantiate(m_effect.dustJumpingEmitter, m_detection.stepCheckObject.position, m_detection.stepCheckObject.rotation);
                }
            }
            else if (m_activation.enableJump && !m_isGrounded && jump)
            {
                // Check for wall jump
                if (m_activation.enableWallJump && m_canWallJump && m_releaseJump && !justJumped
                    || m_activation.enableWallJump && !justJumped && m_releaseJump && m_canWallJumpTime > Time.time)
                {
                    // Add wall jump
                    float horizontalForce = 0;
                    if (!m_wallJumpRight)
                    {
                        horizontalForce = m_movement.wallJumpPush;
                    }
                    else
                    {
                        horizontalForce = -m_movement.wallJumpPush;
                    }
                    m_localRigidbody2D.velocity = Vector2.zero;
                    m_localRigidbody2D.AddRelativeForce(new Vector2(horizontalForce, m_movement.jumpForce * m_movement.wallJump));

                    // add wall jump effect
                    if (m_effect.dustLandingEmitter && m_detection.stepCheckObject)
                    {
                        Instantiate(m_effect.dustLandingEmitter, m_detection.stepCheckObject.position, m_detection.stepCheckObject.rotation);
                    }

                    // Change direction if needed
                    if (m_wallJumpRight == m_isFacingRight)
                    {
                        Flip();
                    }

                    m_canWallJump = false;
                    m_releaseJump = false;
                    m_canDoubleJump = false;
                    m_releaseJumpTime = Time.time;
                    m_jumpStartTime = Time.time;
                    m_longJumpEndTime = Time.time + m_movement.longJumpTime;
                    m_wallJumpEndTime = Time.time + m_wallJumpSlowDuration;
                    m_doubleJumpCount = m_movement.doubleJumpMax;
                }

                // Check for double jump
                else if (m_activation.enableDoubleJump && !justJumped && m_canDoubleJump)
                {
                    // Double jump factor is from 25% to 100% after 1s
                    float doubleJumpFactor = Mathf.Max(0.25f, Mathf.Min(1.0f, Time.time - m_jumpStartTime));

                    // Add double jump
                    m_localRigidbody2D.AddRelativeForce(new Vector2(0f, m_movement.jumpForce * doubleJumpFactor));

                    // double jump effect
                    if (m_effect.dustDoubleJumpEmitter && m_detection.stepCheckObject)
                    {
                        Instantiate(m_effect.dustDoubleJumpEmitter, m_detection.stepCheckObject.position, m_detection.stepCheckObject.rotation);
                    }

                    m_releaseJump = false;
                    m_canDoubleJump = false;
                    m_releaseJumpTime = Time.time;
                    m_jumpStartTime = Time.time;
                    m_longJumpEndTime = Time.time + m_movement.longJumpTime;
                    m_doubleJumpCount--;
                }
                // Check long jump
                else if (m_activation.enableLongJump && Time.time < m_longJumpEndTime)
                {
                    // Add long jump
                    m_localRigidbody2D.AddRelativeForce(new Vector2(0f, Mathf.Lerp(0.0f, m_movement.longJumpForce, (m_longJumpEndTime - Time.time) / m_movement.longJumpTime) * Time.deltaTime));
                }
            }

            if (!m_isGrounded && !jump)
            {
                if (m_doubleJumpCount > 0)
                {
                    m_canDoubleJump = true;
                }
            }

            m_isWallSurfing = false;

            // Wall surfing
            if (!m_isGrounded && m_localRigidbody2D.velocity.y < 0 && m_frontWallDetected && Mathf.Abs(direction) > 0)
            {
                if (m_activation.enableWallSurfing)
                {
                    //m_localRigidbody2D.AddRelativeForce(new Vector2(0f, Mathf.Abs(m_localRigidbody2D.velocity.y) * 10));
                    if (m_movement.wallSurfingFriction == 1f)
                    {
                        m_localRigidbody2D.velocity = new Vector2(m_localRigidbody2D.velocity.x, 0);
                        m_localRigidbody2D.gravityScale = 0f;
                    }
                    else
                    {
                        m_localRigidbody2D.velocity = new Vector2(m_localRigidbody2D.velocity.x, m_localRigidbody2D.velocity.y * (1 - m_movement.wallSurfingFriction));
                    }

                    m_isWallSurfing = true;

                    if (m_effect.dustWallSurfingEmitter && m_detection.frontWallCheckObject && Time.time > m_dustEndTime)
                    {
                        Instantiate(m_effect.dustWallSurfingEmitter, m_detection.frontWallCheckObject.position, m_detection.frontWallCheckObject.rotation);
                        m_dustEndTime = Time.time + m_effect.dustTime;
                    }
                }
            }
            
            // Enable wall jump
            if (m_activation.enableWallJump && !m_isGrounded && m_frontWallDetected)
            {
                m_canWallJump = true;
                m_wallJumpRight = m_isFacingRight;
            } else
            {
                m_canWallJump = false;
            }

            // Walking dust
            if (m_effect.dustWalkingEmitter && m_isGrounded && Time.time > m_dustEndTime && m_detection.stepCheckObject != null && Mathf.Abs(m_localRigidbody2D.velocity.x) > 0.75f)
            {
                Instantiate(m_effect.dustWalkingEmitter, m_detection.stepCheckObject.position, m_detection.stepCheckObject.rotation);
                m_dustEndTime = Time.time + m_effect.dustTime;
            }

            // Set the variables for animator
            if (!m_isGrounded)
            {
                m_isWalking = false;
                m_isRunning = false;
                m_isCrouching = false;

                if (m_localRigidbody2D.velocity.y >= 0)
                {
                    m_isJumping = true;
                    m_isFalling = false;
                }
                else
                {
                    m_isJumping = false;
                    m_isFalling = true;
                }
            }
            else
            {
                m_isJumping = false;
                m_isFalling = false;
                m_isCrouching = crouching;

                if (Mathf.Abs(m_localRigidbody2D.velocity.x) >= 0.05f)
                {
                    m_isWalking = walking;
                    m_isRunning = running;
                }
                else
                {
                    m_isWalking = false;
                    m_isRunning = false;
                }
            }

            if (Mathf.Abs(direction) == 0)
            {
                m_isRunning = false;
                m_isWalking = false;
            }

            // Send all variables to animator
            setAnimatorVariables();

            // Crouching broadcast message
            if (!wasCrouching && m_isCrouching)
            {
                BroadcastMessage("OnMovementCrouch", this, SendMessageOptions.DontRequireReceiver);
            }

            // Standing broadcast message
            if (wasCrouching && !m_isCrouching)
            {
                BroadcastMessage("OnMovementStand", this, SendMessageOptions.DontRequireReceiver);
            }

            // Jumping broadcast message
            if (!wasJumping && m_isJumping)
            {
                BroadcastMessage("OnMovementJump", this, SendMessageOptions.DontRequireReceiver);
            }

            // Land broadcast message
            if (!wasGrounded && m_isGrounded)
            {
                BroadcastMessage("OnMovementLand", this, SendMessageOptions.DontRequireReceiver);
            }

            // Land broadcast message
            if (!wasFalling && m_isFalling)
            {
                BroadcastMessage("OnMovementFall", this, SendMessageOptions.DontRequireReceiver);
            }

            // Run broadcast message
            if (!wasRunning && m_isRunning)
            {
                BroadcastMessage("OnMovementRun", this, SendMessageOptions.DontRequireReceiver);
            }

            // Walk broadcast message
            if (!wasWalking && m_isWalking)
            {
                BroadcastMessage("OnMovementWalk", this, SendMessageOptions.DontRequireReceiver);
            }

            m_lastPosition = transform.position;
        }

        private void setAnimatorVariables()
        {
            if (m_movement == null || m_movement.animators == null || m_movement.animators.Length == 0)
            {
                return;
            }

            foreach (Animator animator in m_movement.animators)
            {
                animator.SetBool("isGrounded", m_isGrounded);
                animator.SetBool("isFalling", m_isFalling);
                if (m_activation.enableJump)
                {
                    animator.SetBool("isJumping", m_isJumping);
                }
                if (m_activation.enableCrouch)
                {
                    animator.SetBool("isCrouching", m_isCrouching);
                }
                if (m_activation.enableWalk)
                {
                    animator.SetBool("isWalking", m_isWalking);
                }
                animator.SetBool("isRunning", m_isRunning);
                animator.SetBool("isWallSurfing", m_isWallSurfing);
                animator.SetBool("isFacingRight", m_isFacingRight);
                animator.SetFloat("speedX", Mathf.Abs(m_localRigidbody2D.velocity.x));
                animator.SetFloat("speedY", Mathf.Abs(m_localRigidbody2D.velocity.y));
            }
        }

        public bool GetIsFacingRight()
        {
            return m_isFacingRight;
        }

        public bool GetIsGrounded()
        {
            return m_isGrounded;
        }

        public bool GetIsFalling()
        {
            return m_isFalling;
        }

        public bool GetIsJumping()
        {
            return m_isJumping;
        }

        public bool GetIsRunning()
        {
            return m_isRunning;
        }

        public bool GetIsWalking()
        {
            return m_isWalking;
        }

        public bool GetIsCrouching()
        {
            return m_isCrouching;
        }
    }

    // END of PixelCrown namespace
}