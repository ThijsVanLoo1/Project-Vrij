using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterReveal : MonoBehaviour
{

    Animator animator;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.orthographicSize >= 60)
        {
            animator.SetTrigger("move");
        }
    }
}
