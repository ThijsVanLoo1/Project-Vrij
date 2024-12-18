using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public string text;
    public float duration = 4;
    bool autoDissapear = false;

    Animator animator;
    TextMeshProUGUI[] textUI;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        textUI = GetComponentsInChildren<TextMeshProUGUI>();
        if (duration == 0) autoDissapear = true;

        SetText();

        yield return new WaitForSeconds(duration);

        Dissapear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText()
    {
        foreach (TextMeshProUGUI textObj in textUI)
        {
            textObj.text = text;
        }
    } 

    public void Dissapear()
    {
        animator.SetTrigger("dissapear");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
