using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderAnimation : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        Leader.OnMoving += Leader_OnMoving;       
    }

    private void OnDisable()
    {
        Leader.OnMoving -= Leader_OnMoving;
    }

    private void Leader_OnMoving()
    {
        animator.SetBool("Moving", true);
    }
}
