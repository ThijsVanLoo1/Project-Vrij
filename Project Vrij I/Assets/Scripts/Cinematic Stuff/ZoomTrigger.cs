using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTrigger : MonoBehaviour
{
    [SerializeField] float targetSize;
    [SerializeField] float zoomSpeed;

    bool zoom;

    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.orthographicSize >= targetSize)
        {
            mainCamera.orthographicSize = targetSize;
            Destroy(gameObject);
        }

        if (zoom)
        {
            ZoomOut();
        }
    }

    void ZoomOut()
    {
        Debug.Log("zoom out");
        mainCamera.orthographicSize += Time.deltaTime * zoomSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player detected");
            zoom = true;
        }
    }
}
