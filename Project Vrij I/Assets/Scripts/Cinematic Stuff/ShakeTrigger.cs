using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTrigger : MonoBehaviour
{
    [SerializeField] ScreenShake shake;
    [SerializeField] float delay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ShakeDelay());
        }
    }

    IEnumerator ShakeDelay()
    {
        yield return new WaitForSeconds(delay);
        shake.start = true;
        Destroy(gameObject);
    }
}
