using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimationStart : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.Play("Dance");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
