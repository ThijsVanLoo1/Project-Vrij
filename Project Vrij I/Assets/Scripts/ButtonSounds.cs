using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip clickSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
