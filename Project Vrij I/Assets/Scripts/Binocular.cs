using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binocular : MonoBehaviour
{
    bool isTouching;
    bool inUse = false;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inUse = true;
                mainCamera.gameObject.GetComponent<CameraFollow>().ZoomToggle();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = false;
        }
    }

}
