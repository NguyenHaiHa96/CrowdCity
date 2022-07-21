using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCState : ScriptableObject
{
    public float speed;

    public virtual void OnStateStart(NPC npc) 
    {
        npc.Agent.speed = this.speed;
    }

    public abstract void Execute(NPC npc, Transform T);
}
