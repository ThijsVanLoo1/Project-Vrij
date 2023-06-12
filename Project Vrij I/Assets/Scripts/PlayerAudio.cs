using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip attachWallSound;
    [SerializeField] AudioClip deattachWallSound;
    [SerializeField] AudioClip landSound;
    [SerializeField] AudioClip[] climbingSound;
    [SerializeField] AudioClip[] steppingSounds;

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

    public void RandomClimb()
    {
        AudioClip randomSound = climbingSound[Random.Range(0, climbingSound.Length)];
        audioSource.PlayOneShot(randomSound, Random.Range(0.3f, 0.6f));
    }

    public void RandomStep()
    {
        AudioClip randomSound = steppingSounds[Random.Range(0, steppingSounds.Length)];
        audioSource.PlayOneShot(randomSound, 0.2f);
    }

    public void Land()
    {
        //audioSource.PlayOneShot(landSound);
    }
}
