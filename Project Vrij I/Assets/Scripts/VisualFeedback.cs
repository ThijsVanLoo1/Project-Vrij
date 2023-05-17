using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualFeedback : MonoBehaviour
{
    [SerializeField] GameObject wallDetectedIndicator;
    [SerializeField] GameObject climbingIndicator;
    [SerializeField] TextMeshPro staminaCounterText;

    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.IsTouchingWall() && !controller.climbingMode)
        {
            wallDetectedIndicator.SetActive(true);
        }
        else
        {
            wallDetectedIndicator.SetActive(false);
        }

        if (controller.climbingMode)
        {
            climbingIndicator.SetActive(true);
        }
        else
        {
            climbingIndicator.SetActive(false);
        }

        staminaCounterText.text = controller.stamina.ToString("F1");
    }
}
