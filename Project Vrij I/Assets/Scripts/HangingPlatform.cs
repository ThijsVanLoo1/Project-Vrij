using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingPlatform : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    GameObject connectedPlatform;
    LineRenderer[] ropePoints;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        connectedPlatform = Instantiate(platformPrefab, transform.position, Quaternion.identity);

        connectedPlatform.GetComponent<DistanceJoint2D>().connectedBody = rb;
        ropePoints = connectedPlatform.GetComponentsInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (LineRenderer point in ropePoints)
        {
            point.SetPosition(0, point.transform.position);
            point.SetPosition(1, transform.position);
        }
    }
}
