using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC State/Following")]
public class FollowingState : NPCState
{
    public override void Execute(NPC npc, Transform leaderTail)
    {
        npc.Agent.SetDestination(leaderTail.position);
    }
}

