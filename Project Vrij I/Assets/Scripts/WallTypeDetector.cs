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

        if (controller.climbingMode)
        {
            if (touchedWall.gameObject.CompareTag("BasicWall"))
            {
                controller.canClimbVertically = true;
                controller.staminaDrainageMultiplier = 1;
            }
            else if (touchedWall.gameObject.CompareTag("SlipWall"))
            {
                controller.canClimbVertically = false;
                controller.GetComponent<Rigidbody2D>().AddForce(slippingAmount * Time.deltaTime * 100);
                controller.staminaDrainageMultiplier = 0;
            }
            else if (touchedWall.gameObject.CompareTag("QualityWall"))
            {
                controller.canClimbVertically = true;
                controller.staminaDrainageMultiplier = 0.5f;
            }
        }
        else
        {
            controller.canClimbVertically = true;
            controller.staminaDrainageMultiplier = 1;
        }
    }
}
