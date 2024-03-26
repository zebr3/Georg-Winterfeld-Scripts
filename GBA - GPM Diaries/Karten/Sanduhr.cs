using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanduhr : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndAnimationMethod()
    {
        animator.SetTrigger("EndAnimation");
    }
}
