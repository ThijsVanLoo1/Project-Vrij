using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachObjectToObject : MonoBehaviour
{
    [SerializeField] GameObject attachedObject;
    [SerializeField] Vector2 objectPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objectPosition = attachedObject.transform.position;
        //transform.position = new Vector2(objectPosition.x, objectPosition.y);
    }
}
