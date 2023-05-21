using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] Vector2 windDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>()) // Check if colliding object has rigidbody
        {
            collision.GetComponent<Rigidbody2D>().AddForce(windDirection * Time.deltaTime * 100); // Add force in desired direction on colliding object (* 100 for easier modification)
        }
    }
}
