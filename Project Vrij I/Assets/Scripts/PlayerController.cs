using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField] float moveSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField] float frictionAmount;
    public float runningMomentum;
    [SerializeField] float runStartSpeed;
    [SerializeField] float maxRunningMomentum;
    [SerializeField] float runningAcceleration;

    [Space] [Header("Jumping Variables")]
    [SerializeField] float jumpForce;
    [Range(1, 3)] [SerializeField] float fallGravityMultiplier;
    [Range(0, 1)] [SerializeField] float jumpCutMultiplier;
    [SerializeField] float jumpCoyoteTime;
    float lastGroundedTime;
    float lastJumpTime;

    [Space]
    [Header("Wall Climbing Variables")]
    [SerializeField] float climbSpeed;
    [SerializeField] float climbAcceleration;
    [SerializeField] float climbDecceleration;
    public bool climbingMode = false;
    [SerializeField] KeyCode wallAttachInput;
    Collider2D touchedWall;

    [Space]
    [Header("Stamina Variables")]
    public float stamina;
    public float maxStamina;
    public float staminaCap;
    [SerializeField] float grabbingStaminaCost;
    [SerializeField] float staminaDrainage;
    [SerializeField] float staminaRestoration;
    [SerializeField] bool instantStaminaRestoration;
    [SerializeField] float maxStaminaDrainage;
    [SerializeField] float maxStaminaRestoration;
    public bool isStunned;
    [SerializeField] float wallJumpStaminaCost;
    [SerializeField] float wallJumpMaxStaminaCost;

    [Space]
    [Header("Wall Type Variables")]
    public float staminaDrainageMultiplier = 1;
    public float climbingSpeedMultiplierX = 1;
    public float climbingSpeedMultiplierY = 1;
    public bool canClimbVertically = true;

    [Space]
    [Header("Additional Settings")]
    public bool easyMode;
    public bool unlockedLeap;

    public bool canInputMovement = true;
    float xInput;
    float yInput;
    float xMovement;
    float yMovement;
    float gravityScale;

    [SerializeField] PauseGame pauseGame;

    Rigidbody2D rb;
    Glider glider;
    CreatePlatform createPlatform;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        glider = GetComponent<Glider>();
        createPlatform = GetComponent<CreatePlatform>();
        gravityScale = rb.gravityScale;
        staminaCap = maxStamina;
        stamina = maxStamina;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (easyMode)
        {
            staminaDrainage = staminaDrainage / 2;
            maxStaminaRestoration = maxStaminaRestoration * 2;
            //climbSpeed = climbSpeed * 1.5f;
        }
    }

    void FixedUpdate()
    {
        if (climbingMode && !pauseGame.isPaused)
        {
            WallMove();
        }
        else if (!pauseGame.isPaused)
        {
            GroundMove();
        }
    }

    void Update()
    {
        if (canInputMovement)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            if (pauseGame.isPaused) { xInput = 0; }
            if(xInput < 0 || xInput > 0 || yInput < 0 || yInput > 0)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

            if (IsGrounded())
            {
                animator.SetFloat("Speed", Mathf.Abs(xInput));//Sets the float from the Animator equal to the positive player speed of this script
            }

            if(xInput > 0f) //Player faces to the right, so original animation
            {
                transform.localScale = new Vector2(1f, 1f);
            }
            else if( xInput < 0f)//Player faces to the left, so animation is flipped
            {
                transform.localScale = new Vector2(-1f, 1f);
            }

            if (canClimbVertically)
            {
                yInput = Input.GetAxisRaw("Vertical");
                if (pauseGame.isPaused) { yInput = 0; }
                //animator.SetFloat("VerticalInput", Mathf.Abs(yInput));
            }

            Run();
            Jump();
            WallClimbing();

            animator.SetBool("isLooking", false);
        }
        else
        {
            xInput = 0;
            yInput = 0;
            animator.SetBool("isLooking", true);
            animator.SetBool("IsFalling", false);
        }
        Friction(); // Prevents player from slipping off edges
    }

    void GroundMove()
    {
        // Horizontal X Movement
        float targetSpeedX = 0; // creates targetSpeed variable
        targetSpeedX = xInput * (moveSpeed + runningMomentum);
        float speedDifX = targetSpeedX - rb.velocity.x; // calculate difference between current and desired velocity
        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? acceleration : decceleration; // change acceleration rate depending on situation
        xMovement = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, 0.9f) * Mathf.Sign(speedDifX); // adds all this shit to movement variable

        rb.AddForce(xMovement * Vector2.right); // applies movement variable as force
    }

    void WallMove()
    {
        // Horizontal X Movement
        float targetSpeedX = 0; // creates targetSpeed variable
        targetSpeedX = xInput * ((climbSpeed + runningMomentum) * climbingSpeedMultiplierX);
        float speedDifX = targetSpeedX - rb.velocity.x; // calculate difference between current and desired velocity
        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? climbAcceleration : climbDecceleration; // change acceleration rate depending on situation
        xMovement = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, 0.9f) * Mathf.Sign(speedDifX); // adds all this shit to movement variable

        rb.AddForce(xMovement * Vector2.right); // applies movement variable as force

        // Vertical X Movement
        float targetSpeedY = 0; // creates targetSpeed variable
        targetSpeedY = yInput * ((climbSpeed + runningMomentum) * climbingSpeedMultiplierY);
        float speedDifY = targetSpeedY - rb.velocity.y; // calculate difference between current and desired velocity
        float accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? climbAcceleration : climbDecceleration; // change acceleration rate depending on situation
        yMovement = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, 0.9f) * Mathf.Sign(speedDifY); // adds all this shit to movement variable

        rb.AddForce(yMovement * Vector2.up); // applies movement variable as force

    }

    void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && xInput != 0 || Input.GetKey(KeyCode.LeftShift) && yInput != 0) // Check for the moment the run button is pressed + if there's movement input
        {
            if (!climbingMode && IsGrounded()) // Check if not climbing is touching ground
            {
                runningMomentum = runStartSpeed;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && xInput != 0 || Input.GetKey(KeyCode.LeftShift) && yInput != 0) // Check if run button is held down + if there's movement input
        { 
            if (!climbingMode && IsGrounded()) // Check if not climbing is touching ground
            {
                runningMomentum += Time.deltaTime * runningAcceleration;
                animator.SetFloat("Running", Mathf.Abs(runningMomentum));
            }
        }
        else 
        { 
            //runningMomentum = 0;
        }
        if ((!Input.GetKey(KeyCode.LeftShift) && IsGrounded()) || xInput == 0)
        {
            runningMomentum = 0;
        }

        if (rb.velocity.x < -0.1f || rb.velocity.x > 0.1f || rb.velocity.y < -0.1f || rb.velocity.y > 0.1f) // Check if player has velocity in any direction
        {

        }
        else
        {
            runningMomentum = 0;
        }

        if (runningMomentum >= maxRunningMomentum) // Cap runningMomentum
        {
            runningMomentum = maxRunningMomentum;
        }

        animator.SetFloat("Running", Mathf.Abs(runningMomentum));
    }

    void Jump()
    {
        if (lastGroundedTime <= jumpCoyoteTime && !climbingMode) // Check if coyote time applies and if not climbing
        {
            if (Input.GetButtonDown("Jump") && lastJumpTime > 0.2f)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                lastJumpTime = 0;
                animator.SetBool("IsJumping", true);//Jump animation is triggered

                if (climbingMode)
                {
                    stamina -= wallJumpStaminaCost;
                    maxStamina -= wallJumpMaxStaminaCost;
                    climbingMode = false;
                }
            }

            if (Input.GetButtonUp("Jump") && !climbingMode)
            {
                rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
            }
        }

        if(rb.velocity.y < -0.2f)
        {
            animator.SetBool("IsJumping", false);//Player is grounded, jump animation stops
        }

        if (IsGrounded())
        {
            lastGroundedTime = 0;
            animator.SetBool("IsFalling", false);
        }
        else
        {
            lastGroundedTime += Time.deltaTime;
            animator.SetBool("IsFalling", true);
        }
        lastJumpTime += Time.deltaTime;

        // Higher gravity after player jumps
        //if (rb.velocity.y < 0)
        //{
        //    rb.gravityScale = gravityScale * fallGravityMultiplier;
        //}
        //else
        //{
        //    rb.gravityScale = gravityScale;
        //}
    }

    void WallClimbing()
    {
        if (Input.GetKeyDown(wallAttachInput) && IsTouchingWall() && stamina > 0 && !isStunned && !createPlatform.buildMode && !pauseGame.isPaused) // Check if: Button pressed, is touching wall, is not stunned, is not in buildMode
        {
            if (!climbingMode)
            {
                //rb.velocity = Vector2.zero;
                stamina -= grabbingStaminaCost;
                animator.SetBool("IsClimbing", true);
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", false);
            }
            else
            {
                animator.SetBool("IsClimbing", false);
            }
            climbingMode = !climbingMode;
        }
        if (!IsTouchingWall()) // if not touching wall
        {
            climbingMode = false;
            animator.SetBool("IsClimbing", false);
        }

        if (climbingMode)
        {
            rb.gravityScale = 0;
            //transform.SetParent(touchedWall.transform);

            stamina -= Time.deltaTime * staminaDrainage * staminaDrainageMultiplier;
            maxStamina -= Time.deltaTime * maxStaminaDrainage * staminaDrainageMultiplier;
        }
        else
        {
            if (!glider.glidingMode) { rb.gravityScale = gravityScale; } // When not gliding, reset gravityScale
            if (transform.parent != null)
            {
                if (transform.parent.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    transform.SetParent(null);
                }
            }

            if (IsGrounded())
            {
                if (instantStaminaRestoration)
                {
                    stamina = maxStamina;
                }
                else
                {
                    stamina += Time.deltaTime * staminaRestoration;
                }
                maxStamina += Time.deltaTime * maxStaminaRestoration;
            }
            animator.SetBool("IsClimbing", false);
        }

        if (stamina >= maxStamina)
        {
            stamina = maxStamina;
        }
        if (stamina <= 0)
        {
            climbingMode = false;
            stamina = 0;
        }
        if (maxStamina >= staminaCap)
        {
            maxStamina = staminaCap;
        }

        // Wall Leaping
        if (climbingMode && unlockedLeap) // Check if climbing and leap is unlocked
        {
            if (Input.GetButtonDown("Jump") && xInput != 0 || Input.GetButtonDown("Jump") && yInput != 0) // Check if jump button is pressed + if there's movement input
            {
                if (yInput > 0) // Up Input
                {
                    stamina -= wallJumpStaminaCost;
                    maxStamina -= wallJumpMaxStaminaCost;
                    climbingMode = false;
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
                else if (yInput < 0) // Down Input
                {
                    stamina -= wallJumpStaminaCost;
                    maxStamina -= wallJumpMaxStaminaCost;
                    climbingMode = false;
                    rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
                }
                else if (xInput < 0) // Left Input
                {
                    stamina -= wallJumpStaminaCost;
                    maxStamina -= wallJumpMaxStaminaCost;
                    climbingMode = false;
                    rb.AddForce(Vector2.left * jumpForce * 2, ForceMode2D.Impulse);
                }
                else if (xInput > 0) // Right Input
                {
                    stamina -= wallJumpStaminaCost;
                    maxStamina -= wallJumpMaxStaminaCost;
                    climbingMode = false;
                    rb.AddForce(Vector2.right * jumpForce * 2, ForceMode2D.Impulse);
                }
                animator.SetBool("IsClimbing", false);
                animator.SetBool("IsJumping", true); //Jump animation is triggered
            }
        }
    }

    void Friction()
    {
        if (lastGroundedTime <= 0.05f && Mathf.Abs(xInput) < 0.01f) // Check if player is grounded and no input is being applied
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse); // does stuff idk
        }
    }

    public void GetHit(float staminaAmount, float stunTime)
    {
        if (!isStunned)
        {
            animator.SetBool("IsClimbing", false);
            isStunned = true;
            climbingMode = false;
            stamina -= staminaAmount;
            StartCoroutine(Stun(stunTime));
        }
    }
    IEnumerator Stun(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
    }

    // Checks if player is grounded
    [Space]
    [Header("Ground Check Variables")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Checks if player is touching background wall
    [Space]
    [Header("Wall Check Variables")]
    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckRadius;
    [SerializeField] LayerMask wallLayer;

    public bool IsTouchingWall()
    {
        touchedWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
    }
}
