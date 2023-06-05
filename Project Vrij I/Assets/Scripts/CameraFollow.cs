using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed;
    Transform player;
    float distance;

    float standardSize;
    public float zoomedOutSize;
    public float zoomSpeed;
    public bool zoomedOut;

    Camera thisCamera;
    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        thisCamera = GetComponent<Camera>();
;
        standardSize = thisCamera.orthographicSize;

        controller = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);

        distance = Vector2.Distance(transform.position, targetPosition);

        transform.position = new Vector3(targetPosition.x, targetPosition.y, -10);

        //CameraScroll();
        //Zoom();
    }

    void CameraScroll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            thisCamera.orthographicSize++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            thisCamera.orthographicSize--;
        }
    }

    public void ZoomToggle()
    {
        zoomedOut = !zoomedOut;
        LockOrUnlockMovement();
    }

    void Zoom()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ZoomToggle();
        }

        if (zoomedOut)
        {
            thisCamera.orthographicSize += Time.deltaTime * zoomSpeed;
            if (thisCamera.orthographicSize >= zoomedOutSize)
            {
                thisCamera.orthographicSize = zoomedOutSize;
            }
        }
        else
        {
            thisCamera.orthographicSize -= Time.deltaTime * zoomSpeed;
            if (thisCamera.orthographicSize <= standardSize)
            {
                thisCamera.orthographicSize = standardSize;
            }
        }
    }

    void LockOrUnlockMovement()
    {
        if (zoomedOut)
        {
            controller.canInputMovement = false;
        }
        else
        {
            controller.canInputMovement = true;
        }
    }
}
