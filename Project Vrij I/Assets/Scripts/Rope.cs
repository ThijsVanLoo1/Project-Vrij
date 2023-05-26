using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] GameObject attachedRopePrefab;
    [SerializeField] KeyCode placeRopeInput;
    [SerializeField] KeyCode holdRopeInput;

    GameObject placedRope;
    float distance;
    bool holdingRope;

    DistanceJoint2D joint;
    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (placedRope != null)
        {
            distance = Vector2.Distance(transform.position, placedRope.transform.position);
        }

        RopeHolding();
        DrawLine();

        if (Input.GetKeyDown(placeRopeInput))
        {
            if (placedRope != null) { Destroy(placedRope); } // Destroy any already placed ropes
            placedRope = Instantiate(attachedRopePrefab, transform.position, Quaternion.identity); // Place new rope
        }
    }

    void RopeHolding()
    {
        if (Input.GetKey(holdRopeInput))
        {
            controller.holdingRope = true;
        }
        else
        {
            controller.holdingRope = false;
        }

        if (controller.holdingRope)
        {
            controller.canInputMovement = false;
            controller.climbingMode = false;
            placedRope.GetComponent<DistanceJoint2D>().distance = distance;
        }
    }

    void DrawLine()
    {
        if (placedRope != null)
        {
            placedRope.GetComponent<LineRenderer>().SetPosition(0, placedRope.transform.position);
            placedRope.GetComponent<LineRenderer>().SetPosition(1, transform.position);
        }
    }
}
