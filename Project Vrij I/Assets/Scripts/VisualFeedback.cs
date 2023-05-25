using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualFeedback : MonoBehaviour
{
    [SerializeField] GameObject wallDetectedIndicator;
    [SerializeField] GameObject climbingIndicator;
    [SerializeField] TextMeshPro staminaCounterText;
    [SerializeField] GameObject stunIndicator;
    [SerializeField] GameObject glidingIndicator;

    [SerializeField] Transform staminaBarFill;
    [SerializeField] Transform maxStaminaBarFill;
    Vector2 staminaScale;
    Vector2 maxStaminaScale;

    PlayerController controller;
    Glider glider;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        glider = GetComponent<Glider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.IsTouchingWall() && !controller.climbingMode && !controller.isStunned && controller.stamina != 0)
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

        if (controller.isStunned)
        {
            stunIndicator.SetActive(true);
        }
        else
        {
            stunIndicator.SetActive(false);
        }

        if (glider.glidingMode)
        {
            glidingIndicator.SetActive(true);
        }
        else
        {
            glidingIndicator.SetActive(false);
        }

        staminaCounterText.text = controller.stamina.ToString("F1");
        StaminaBar();
    }

    void StaminaBar()
    {
        staminaScale = new Vector2(controller.stamina / 10, staminaBarFill.transform.localScale.y);
        staminaBarFill.localScale = staminaScale;

        maxStaminaScale = new Vector2(controller.maxStamina / 10, maxStaminaBarFill.transform.localScale.y);
        maxStaminaBarFill.localScale = maxStaminaScale;
    }
}
