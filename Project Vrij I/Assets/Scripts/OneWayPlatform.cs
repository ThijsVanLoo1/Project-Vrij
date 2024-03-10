using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    GameObject touchingPlayer;
    bool playerTouching;

    PlatformEffector2D effector;
    Collider2D platformCollider;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        platformCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            if (touchingPlayer != null && playerTouching)
            {
                touchingPlayer.GetComponent<PlayerController>().climbingMode = true;
            }
            platformCollider.enabled = false;
        }
        else
        {
            platformCollider.enabled = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = collision.gameObject;
            playerTouching = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTouching = false;
        }
    }
}
