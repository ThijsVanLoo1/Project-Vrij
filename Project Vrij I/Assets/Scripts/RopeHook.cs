using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHook : MonoBehaviour
{
    DistanceJoint2D joint;
    PlayerController playerController;
    Rigidbody2D playerRb;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerRb = playerController.GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        joint.connectedBody = playerRb;
        line = GetComponent<LineRenderer>();

        playerController.attachedToRope = true;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, playerController.transform.position);
    }
}
