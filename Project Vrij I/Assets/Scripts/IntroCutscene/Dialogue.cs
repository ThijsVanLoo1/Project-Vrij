using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public float duration = 4;

    Animator animator;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        Appear();

        yield return new WaitForSeconds(duration);

        Dissapear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Appear()
    {
        animator.SetTrigger("appear");
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
