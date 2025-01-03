using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 5;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void OpenFeedbackForm()
    {
        Debug.Log("Opening URL");
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSfq8z11gOwMz_q0oykqbKmpiAE2li6Dwr4IxcClhMGem1lDng/viewform");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
