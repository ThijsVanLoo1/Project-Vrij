using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLeap : MonoBehaviour
{
    [SerializeField] GameObject unlockUI;
    ParticleSystem particles;

    bool unlocked;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !unlocked)
        {
            unlocked = true;
            unlockUI.SetActive(true);
            collision.gameObject.GetComponent<PlayerController>().unlockedLeap = true;
            particles.Stop();
            //Destroy(gameObject);
        }
    }
}
