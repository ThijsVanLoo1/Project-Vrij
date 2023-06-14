using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float length, startPos;
    [SerializeField] GameObject cam;
    [SerializeField] float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = (cam.transform.position.y * parallaxEffect);
        transform.position = new Vector3(transform.position.x, startPos + distance, transform.position.z);
    }
}
