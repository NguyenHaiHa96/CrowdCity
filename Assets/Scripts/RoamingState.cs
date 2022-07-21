using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "NPC State/Roaming")]
public class RoamingState : NPCState
{
    [SerializeField] private float range;
    [SerializeField] private float timeToChangePosition;

    private float timer;

    private void MoveToRandomPosition(NPC npc)
    {
        Vector3 randomPosition;
        timer += Time.deltaTime;
        if (timer > timeToChangePosition)
        {
            if (RandomPosition(npc.transform.position, range, out randomPosition))
            {
                Debug.DrawRay(randomPosition, Vector3.up, Color.black, 1);
                Rotate(npc, randomPosition);
                npc.Agent.SetDestination(randomPosition);
                timer = 0;
            }
        }
    }

    bool RandomPosition(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
    
    private void Rotate(NPC npc, Vector3 position) 
    {
        Vector3 lookAtPosition = new Vector3(position.x - npc.transform.position.x,
                    npc.transform.position.y,
                    position.z - npc.transform.position.z);
        npc.Rotation = Quaternion.LookRotation(lookAtPosition); 
        npc.Rb.MoveRotation(Quaternion.Slerp(npc.transform.rotation, 
            npc.Rotation,
            npc.RotationSpeed * Time.deltaTime));
    }

    public override void Execute(NPC npc, Transform T)
    {
        MoveToRandomPosition(npc); 
    }
}
