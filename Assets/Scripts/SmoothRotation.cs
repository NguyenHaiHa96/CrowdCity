using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotation : MonoBehaviour
{
    //[SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = .01f;

    private Vector3 targetPosition;
    private Quaternion rotationGoal;
    private Vector3 direction;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SetTargetPosition();
        }
    }

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (!hit.transform.gameObject.CompareTag("Player"))
            {
                targetPosition = hit.point;

                direction = new Vector3(targetPosition.x - this.transform.position.x,
                    this.transform.position.y,
                    targetPosition.z - this.transform.position.z).normalized;
                Debug.Log(direction);

                rotationGoal = Quaternion.LookRotation(direction);
            }
        }
    }

    private void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, rotationSpeed);
    }
}
