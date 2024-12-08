using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);

                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);

                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Application.Quit();
            }
        }
    }
}
