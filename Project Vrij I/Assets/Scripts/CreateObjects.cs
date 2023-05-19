using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjects : MonoBehaviour
{
    [SerializeField] KeyCode keyInput;
    [SerializeField] Transform spawnPoint;

    [SerializeField] GameObject hangingPlatformPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyInput))
        {
            Instantiate(hangingPlatformPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
