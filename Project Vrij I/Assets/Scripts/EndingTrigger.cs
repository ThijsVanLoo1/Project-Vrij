using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] Animator endingAnimator;

    ParticleSystem particles;

    bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !triggered)
        {
            triggered = true;
            endingAnimator.SetTrigger("start");
            collision.gameObject.GetComponent<PlayerController>().canInputMovement = false;
            particles.Stop();
        }
    }
}
