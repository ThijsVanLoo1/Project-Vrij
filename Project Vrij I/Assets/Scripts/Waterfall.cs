using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    [SerializeField] float waterStrength;
    [SerializeField] AudioClip waterSplashSound;

    //AudioSource audioSource;
    Vector2 waterDirection;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        waterDirection = new Vector2(0, -waterStrength);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(waterSplashSound, collision.transform.position);
            //audioSource.PlayOneShot(waterSplashSound);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>()) // Check if colliding object has rigidbody
        {
            collision.GetComponent<Rigidbody2D>().AddForce(waterDirection * Time.deltaTime * 100); // Add force in desired direction on colliding object (* 100 for easier modification)
        }
    }
}
