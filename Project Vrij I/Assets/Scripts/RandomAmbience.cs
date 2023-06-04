using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAmbience : MonoBehaviour
{
    [SerializeField] AudioClip[] ambienceSounds;
    [SerializeField] float minTime = 30;
    [SerializeField] float maxTime = 60;

    AudioClip randomSound;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomSound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayRandomSound()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        randomSound = ambienceSounds[Random.Range(0, ambienceSounds.Length)];
        audioSource.PlayOneShot(randomSound);
        StartCoroutine(PlayRandomSound());
    }
}
