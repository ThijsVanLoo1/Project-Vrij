using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] float glidingGravity;
    [SerializeField] KeyCode glideInput;
    public bool glidingMode;

    Rigidbody2D rb;
    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(glideInput))
        {
            controller.climbingMode = false;
            glidingMode = !glidingMode;
            if (glidingMode && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        if (controller.IsGrounded())
        {
            glidingMode = false;
        }
        if (glidingMode)
        {
            if (controller.climbingMode)
            {
                glidingMode = false;
            }
            Gliding();
        }
    }

    void Gliding()
    {
        rb.gravityScale = glidingGravity;
    }
}
