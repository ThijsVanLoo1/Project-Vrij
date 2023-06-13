using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] GameObject wallDetectedObject;
    [SerializeField] GameObject noWallDetectedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTouchingWall())
        {
            wallDetectedObject.SetActive(true);
            noWallDetectedObject.SetActive(false);
        }
        else
        {
            wallDetectedObject.SetActive(false);
            noWallDetectedObject.SetActive(true);
        }
    }

    public bool IsTouchingWall()
    {
        return Physics2D.OverlapCircle(transform.position, 0.1f, wallLayer);
    }
}
