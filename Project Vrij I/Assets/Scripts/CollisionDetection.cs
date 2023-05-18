using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Obstacle":
                ObstacleHit();
                break;
            default:
                break;
        }
    }

    void ObstacleHit()
    {
        controller.climbingMode = false;
        controller.stamina -= 3;
    }
}
