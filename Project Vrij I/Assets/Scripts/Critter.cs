using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour
{
    [SerializeField] float spottingDistance;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject fleeingPosition;
    Transform player;
    Vector2 fleePoint;
    Vector2 pos;
    float distance;
    bool isFleeing;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        fleePoint = fleeingPosition.transform.position;
        Destroy(fleeingPosition);

        float randomFLoat = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, randomFLoat);
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        distance = Vector2.Distance(transform.position, player.position);
        if (distance <= spottingDistance)
        {
            isFleeing = true;
        }

        if (isFleeing)
        {
            FleeToPosition();
        }
    }

    void FleeToPosition()
    {
        RotateToPosition();
        transform.position = Vector2.MoveTowards(transform.position, fleePoint, moveSpeed * Time.deltaTime);

        if (pos == fleePoint) Destroy(gameObject);
    }

    void RotateToPosition()
    {
        Vector2 targetPos = fleePoint;
        Vector2 thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
