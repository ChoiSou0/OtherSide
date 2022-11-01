using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Controller 
{
    private Animator animator;
    private void Awake() => animator = transform.GetChild(1).GetComponent<Animator>();
    private void AnimationCheck()
    {
        animator.SetBool("isWalk", isWalking);

        if (currentNode.GetComponent<Walkable>().isStair)
            animator.SetTrigger("isStair");
    }
}
