using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyModeToggle : MonoBehaviour
{
    [SerializeField] GameObject enabledText;
    [SerializeField] GameObject disabledText;

    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            controller.easyMode = !controller.easyMode;
            if (controller.easyMode)
            {
                enabledText.SetActive(true);
                disabledText.SetActive(false);
            }
            else
            {
                enabledText.SetActive(false);
                disabledText.SetActive(true);
            }
        }
    }
}
