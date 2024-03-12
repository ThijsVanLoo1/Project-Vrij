using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlatform : MonoBehaviour
{
    [SerializeField] KeyCode keyInput;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LayerMask wallLayer;

    [SerializeField] GameObject hangingPlatformPrefab;
    [SerializeField] AudioClip creationSound;
    [SerializeField] GameObject restockNotification;
    [SerializeField] GameObject outOfPlatformsNotification;

    public int platformMaterials;
    [SerializeField] int maxPlatformMaterials;

    public bool buildMode;

    public bool unlockedPlatform;

    PlayerController controller;
    PauseGame pauseGame;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        pauseGame = FindObjectOfType<PauseGame>();

        platformMaterials = maxPlatformMaterials;
;   }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keyInput) && unlockedPlatform && !pauseGame.isPaused)
        {
            buildMode = true;
            spawnPoint.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (IsTouchingWall())
                {
                    if (platformMaterials > 0)
                    {
                        PutUpPlatform();
                    }
                    else
                    {
                        outOfPlatformsNotification.SetActive(true);
                    }
                }
            }
        }
        else
        {
            buildMode = false;
            spawnPoint.gameObject.SetActive(false);
        }

        if (platformMaterials > maxPlatformMaterials)
        {
            platformMaterials = maxPlatformMaterials;
        }
        if (platformMaterials < 0)
        {
            platformMaterials = 0;
        }
    }

    void PutUpPlatform()
    {
        AudioSource.PlayClipAtPoint(creationSound, transform.position);
        platformMaterials--;
        Instantiate(hangingPlatformPrefab, spawnPoint.position, Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Restock") && platformMaterials != maxPlatformMaterials)
        {
            platformMaterials = maxPlatformMaterials;
            restockNotification.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Restock") && platformMaterials != maxPlatformMaterials)
        {
            platformMaterials = maxPlatformMaterials;
            restockNotification.SetActive(true);
        }
    }


    public bool IsTouchingWall()
    {
        return Physics2D.OverlapCircle(spawnPoint.position, 0.1f, wallLayer);
    }
}
