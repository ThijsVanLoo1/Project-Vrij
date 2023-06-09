using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithCamera : MonoBehaviour
{
    [SerializeField] float scaleStrength;

    float startCameraSize;
    float targetSize;

    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        startCameraSize = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        targetSize = mainCamera.orthographicSize - startCameraSize + 1 * scaleStrength;

        transform.localScale = new Vector2(targetSize, targetSize);
    }
}
