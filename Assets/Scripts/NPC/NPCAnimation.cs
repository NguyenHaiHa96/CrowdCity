using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    private NPC npc;
    private Animator animator;

    void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        npc = GetComponent<NPC>();
        npc.OnFollowingStateChange += Npc_OnFollowingStateChange;
    }
    void OnDisable()
    {
        npc.OnFollowingStateChange -= Npc_OnFollowingStateChange;
    }

    private void Npc_OnFollowingStateChange(NPC npc)
    {
        if (npc.IsFollowing)
            animator.SetBool("IsFollowing", true);
    }
}
