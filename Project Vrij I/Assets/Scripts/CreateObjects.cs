using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjects : MonoBehaviour
{
    [SerializeField] KeyCode keyInput;
    [SerializeField] Transform spawnPoint;

    [SerializeField] GameObject hangingPlatformPrefab;

    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
;    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyInput) && controller.climbingMode)
        {
            Instantiate(hangingPlatformPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
