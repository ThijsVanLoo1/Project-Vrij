using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualFeedback : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color stunnedColor;

    [SerializeField] GameObject wallDetectedIndicator;
    [SerializeField] GameObject climbingIndicator;
    [SerializeField] TextMeshPro staminaCounterText;
    [SerializeField] GameObject stunIndicator;
    [SerializeField] GameObject glidingIndicator;

    [SerializeField] TextMeshPro materialCounter;

    [SerializeField] Transform staminaBarFill;
    [SerializeField] Transform maxStaminaBarFill;
    [SerializeField] GameObject staminaMeter;
    Vector2 staminaScale;
    Vector2 maxStaminaScale;

    PlayerController controller;
    Glider glider;
    CreatePlatform platform;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        glider = GetComponent<Glider>();
        platform = GetComponent<CreatePlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.IsTouchingWall() && !controller.climbingMode && !controller.isStunned && controller.stamina != 0 && !platform.buildMode)
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
            spriteRenderer.color = stunnedColor;
        }
        else
        {
            spriteRenderer.color = new Color(255, 255, 255);
        }

        //if (platform.buildMode)
        //{
        //    materialCounter.gameObject.SetActive(true);
        //    materialCounter.text = platform.platformMaterials.ToString();
        //}
        //else
        //{
        //    materialCounter.gameObject.SetActive(false);
        //}

        staminaCounterText.text = controller.stamina.ToString("F1");
        //StaminaBar();
    }

    void StaminaBar()
    {
        staminaScale = new Vector2(controller.stamina / 10, staminaBarFill.transform.localScale.y);
        staminaBarFill.localScale = staminaScale;

        maxStaminaScale = new Vector2(controller.maxStamina / 10, maxStaminaBarFill.transform.localScale.y);
        maxStaminaBarFill.localScale = maxStaminaScale;

        if (controller.maxStamina >= controller.staminaCap)
        {
            staminaMeter.SetActive(false);
        }
        else
        {
            staminaMeter.SetActive(true);
        }
    }
}
