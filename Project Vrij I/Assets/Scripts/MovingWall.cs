using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] int startingPoint;
    [SerializeField] bool onlyMoveWhenTouching;
    [SerializeField] Transform[] points;

    int i;
    bool isClimbing;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (!onlyMoveWhenTouching)
        {
            MoveObject();
        }
        else if (isClimbing)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, moveSpeed * Time.deltaTime);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerController>().climbingMode)
            {
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isClimbing = false;
        }
    }
}
