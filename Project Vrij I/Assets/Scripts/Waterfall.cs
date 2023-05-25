using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    [SerializeField] float waterStrength;

    Vector2 waterDirection;

    // Start is called before the first frame update
    void Start()
    {
        waterDirection = new Vector2(0, -waterStrength);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>()) // Check if colliding object has rigidbody
        {
            collision.GetComponent<Rigidbody2D>().AddForce(waterDirection * Time.deltaTime * 100); // Add force in desired direction on colliding object (* 100 for easier modification)
        }
    }
}
