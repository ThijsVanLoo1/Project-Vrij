using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float spottingDistance;
    [SerializeField] float moveSpeed;
    Transform player;
    Transform scarePosition;
    float distance;
    bool isFleeing;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);
        if (distance <= spottingDistance)
        {
            if (!isFleeing)
            {
                scarePosition = player;
            }
            isFleeing = true;
        }

        if (isFleeing)
        {
            FlyAway();
        }

        if (distance > 1000)
        {
            Destroy(gameObject);
        }
    }

    void FlyAway()
    {
        transform.position = Vector2.MoveTowards(transform.position, scarePosition.position, -moveSpeed * Time.deltaTime);
    }
}
