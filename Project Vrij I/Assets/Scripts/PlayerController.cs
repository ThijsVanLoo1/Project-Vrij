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
    [SerializeField] float maxRunningMomentum;

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
    [SerializeField] float maxStamina;
    [SerializeField] float staminaDrainage;
    [SerializeField] float staminaRestoration;


    float xInput;
    float yInput;
    float xMovement;
    float yMovement;
    float gravityScale;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
        stamina = maxStamina;
    }

    void FixedUpdate()
    {
        if (climbingMode)
        {
            WallMove();
        }
        else
        {
            GroundMove();
        }
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        Run();
        Jump();
        WallClimbing();
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
        targetSpeedX = xInput * (climbSpeed + runningMomentum);
        float speedDifX = targetSpeedX - rb.velocity.x; // calculate difference between current and desired velocity
        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? climbAcceleration : climbDecceleration; // change acceleration rate depending on situation
        xMovement = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, 0.9f) * Mathf.Sign(speedDifX); // adds all this shit to movement variable

        rb.AddForce(xMovement * Vector2.right); // applies movement variable as force

        float targetSpeedY = 0; // creates targetSpeed variable
        targetSpeedY = yInput * (climbSpeed + runningMomentum);
        float speedDifY = targetSpeedY - rb.velocity.y; // calculate difference between current and desired velocity
        float accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? climbAcceleration : climbDecceleration; // change acceleration rate depending on situation
        yMovement = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, 0.9f) * Mathf.Sign(speedDifY); // adds all this shit to movement variable

        rb.AddForce(yMovement * Vector2.up); // applies movement variable as force
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && xInput != 0 || Input.GetKey(KeyCode.LeftShift) && yInput != 0) // Check if run button is held down + if there's movement input
        { 
            if (!climbingMode && IsGrounded()) // Check if not climbing is touching ground
            {
                runningMomentum += Time.deltaTime;
            }
        }
        else 
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
    }

    void Jump()
    {
        if (lastGroundedTime <= jumpCoyoteTime || climbingMode) // Check if coyote time applies
        {
            if (Input.GetButtonDown("Jump") && lastJumpTime > 0.2f)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                lastJumpTime = 0;

                if (climbingMode)
                {
                    stamina -= 3;
                    climbingMode = false;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
            }
        }

        if (IsGrounded())
        {
            lastGroundedTime = 0;
        }
        else
        {
            lastGroundedTime += Time.deltaTime;
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
        if (Input.GetKeyDown(wallAttachInput) && IsTouchingWall() && stamina > 0) // Check if button is pressed while touching wall
        {
            if (!climbingMode)
            {
                rb.velocity = Vector2.zero;
            }
            climbingMode = !climbingMode;
        }
        if (!IsTouchingWall()) // if not touching wall
        {
            climbingMode = false;
        }

        if (climbingMode)
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale -= Time.deltaTime;
            }
            else
            {
                rb.gravityScale = 0;
            }
            transform.parent = touchedWall.transform;

            stamina -= Time.deltaTime * staminaDrainage;
        }
        else
        {
            rb.gravityScale = gravityScale;
            transform.parent = null;
            if (IsGrounded())
            {
                stamina += Time.deltaTime * staminaRestoration;
            }
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
