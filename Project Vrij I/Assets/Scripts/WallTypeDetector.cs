using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTypeDetector : MonoBehaviour
{
    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckRadius;
    [SerializeField] LayerMask wallLayer;

    [SerializeField] Vector2 slippingAmount;

    Collider2D touchedWall;
    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        touchedWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);

        if (touchedWall != null && controller.climbingMode)
        {
            if (touchedWall.gameObject.CompareTag("RoughWall"))
            {
                controller.staminaDrainageMultiplier = 1;
                controller.climbingSpeedMultiplierX = 0.3f;
                controller.climbingSpeedMultiplierY = 0.3f;
            }
            else if (touchedWall.gameObject.CompareTag("SlipWall"))
            {
                controller.staminaDrainageMultiplier = 1;
                controller.climbingSpeedMultiplierX = 1f;
                controller.climbingSpeedMultiplierY = 0;
                SlipDown(6);
            }
            else if (touchedWall.gameObject.CompareTag("BasicWall"))
            {
                controller.staminaDrainageMultiplier = 1;
                controller.climbingSpeedMultiplierX = 1;
                controller.climbingSpeedMultiplierY = 1;
            }
            else if (touchedWall.gameObject.CompareTag("QualityWall"))
            {
                controller.staminaDrainageMultiplier = 0;
                controller.climbingSpeedMultiplierX = 1.5f;
                controller.climbingSpeedMultiplierY = 1.5f;
            }
        }
        else
        {
            controller.staminaDrainageMultiplier = 1;
        }
    }

    void SlipDown(float slippingForce)
    {
        Vector2 slippingDirection = new Vector2(0, -slippingForce);
        controller.GetComponent<Rigidbody2D>().AddForce(slippingDirection * Time.deltaTime * 100);
    }
}
