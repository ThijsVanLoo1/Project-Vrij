using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] AudioClip damageSound;

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


    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                ObstacleHit();
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
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
        AudioSource.PlayClipAtPoint(damageSound, transform.position);
        controller.GetHit(3, 1.5f);
    }

    void WallAttributes(float slippingForce, float staminaDrainageMultiplier, bool canClimbVertically)
    {
        controller.canClimbVertically = canClimbVertically;
        Vector2 slippingVector2 = new Vector2(transform.position.x, slippingForce);
        controller.GetComponent<Rigidbody2D>().AddForce(-slippingVector2 * Time.deltaTime * 100);
        controller.staminaDrainageMultiplier = staminaDrainageMultiplier;
    }
}
