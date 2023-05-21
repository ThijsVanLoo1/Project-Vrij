using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingWall : MonoBehaviour
{
    [SerializeField] float crumblingDuration;
    [SerializeField] float fallSpeed;
    [SerializeField] float momentumMultiplier;
    float momentum = 1;
    float currentDuration;

    bool isFalling;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        currentDuration = crumblingDuration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentDuration <= 0)
        {
            isFalling = true;
            transform.Translate(Vector2.down * Time.deltaTime * fallSpeed * momentum);
            momentum += Time.deltaTime * momentumMultiplier;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerController>().climbingMode && !isFalling)
            {
                currentDuration -= Time.deltaTime;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
        }
    }
}
