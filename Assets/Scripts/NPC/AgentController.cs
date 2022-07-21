using UnityEngine.AI;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                agent.SetDestination(raycastHit.point);
            }
        }
    }
}
