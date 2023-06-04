using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip attachWallSound;
    [SerializeField] AudioClip deattachWallSound;
    [SerializeField] AudioClip landSound;

    PlayerController controller;
    AudioSource audioSource;

            bool touchingGround;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.IsGrounded())
        {
            if (!touchingGround)
            {
                Land();
            }
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }
    }

    public void Jump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void Attach()
    {
        audioSource.PlayOneShot(attachWallSound);
    }

    public void Deattach()
    {
        audioSource.PlayOneShot(deattachWallSound);
    }

    public void Land()
    {
        //audioSource.PlayOneShot(landSound);
    }
}
