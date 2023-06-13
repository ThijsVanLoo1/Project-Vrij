using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    bool started;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !started)
        {
            started = true;
            GetComponent<AudioSource>().Play();
            Invoke("ResumeTime", 4);
        }
    }

    void ResumeTime()
    {
        Time.timeScale = 0;
    }
}
