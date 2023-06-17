using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] Animator endingAnimator;
    [SerializeField] Transform eyeTransform;

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
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            controller.canInputMovement = false;
            controller.stamina = controller.staminaCap;
            FindObjectOfType<CanvasStats>().StaminaFull();
            FindObjectOfType<CanvasStats>().enabled = false;
            particles.Stop();
        }
    }

    public void MoveCameraToEye()
    {
        Camera.main.GetComponent<CameraFollow>().followPlayer = false;
        Camera.main.GetComponent<CameraFollow>().targetPosition = Vector3.MoveTowards(transform.position, eyeTransform.position, 10 * Time.deltaTime);
    }
}
