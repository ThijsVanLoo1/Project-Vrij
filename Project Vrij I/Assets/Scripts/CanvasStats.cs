using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasStats : MonoBehaviour
{
    [SerializeField] Image staminaBarFill;
    [SerializeField] Image maxStaminaBarFill;
    [SerializeField] GameObject staminaMeter;

    [SerializeField] GameObject platformIcon1;
    [SerializeField] GameObject platformIcon2;
    [SerializeField] GameObject platformCounter;

    PlayerController controller;
    CreatePlatform platform;
    [SerializeField] Animator staminaAnimator;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
        platform = controller.gameObject.GetComponent<CreatePlatform>();

        staminaMeter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StaminaBar();
        PlatformCounter();
    }

    void StaminaBar()
    {
        staminaBarFill.fillAmount = controller.stamina / 10;
        maxStaminaBarFill.fillAmount = controller.maxStamina / 10;

        if (controller.maxStamina >= controller.staminaCap)
        {
            staminaAnimator.SetTrigger("full");
            //staminaMeter.SetActive(false);
        }
        else
        {
            staminaMeter.SetActive(true);
        }
    }

    void PlatformCounter()
    {
        if (platform.buildMode)
        {
            platformCounter.SetActive(true);

            if (platform.platformMaterials > 0)
            {
                platformIcon1.SetActive(true);

                if (platform.platformMaterials > 1)
                {
                    platformIcon2.SetActive(true);
                }
                else
                {
                    platformIcon2.SetActive(false);
                }
            }
            else
            {
                platformIcon1.SetActive(false);
            }
        }
        else
        {
            platformCounter.SetActive(false);
        }
    }
}
