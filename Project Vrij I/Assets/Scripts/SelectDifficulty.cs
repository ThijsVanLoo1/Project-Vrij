using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectDifficulty : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EasyMode()
    {
        SceneManager.LoadScene("EasyWorld");
    }

    public void NormalMode()
    {
        SceneManager.LoadScene("World");
    }
}
