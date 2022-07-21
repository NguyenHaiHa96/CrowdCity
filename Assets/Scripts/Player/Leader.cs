using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public static Action<GameObject, Transform> OnAddedNPC;
    public static Action OnMoving;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform leaderTail;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float radius;

    private Vector3 targetPosition;
    private Vector3 lookAtPosition;
    private Vector3 moveDirection;
    private Vector3 velocity;
    private Quaternion playerRotation;
    private bool moving;
    private int followers;

    public Transform LeaderTail => leaderTail;

    private void OnEnable()
    {
        moving = false;
    }   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            SetTargetPosition();
        if (moving)
        {
            OnMoving();
            Rotate();
            CalculateVelocity();
            CheckCollider();
        }    
    }

    private void FixedUpdate()
    {
        rb.velocity = this.velocity * Time.deltaTime;
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
                lookAtPosition = new Vector3(targetPosition.x - this.transform.position.x,
                    this.transform.position.y,
                    targetPosition.z - this.transform.position.z);
                moving = true;
                playerRotation = Quaternion.LookRotation(lookAtPosition);       
            }             
        }
    }

    private void Rotate()
    {
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, 
            playerRotation,
            rotationSpeed * Time.deltaTime));
    }

    private void CalculateVelocity()
    {
        moveDirection = lookAtPosition.normalized;
        velocity = moveDirection * moveSpeed;
    }

    private void CheckCollider()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
        foreach (var collider in hitColliders)
        { 
            if (!collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.CompareTag("NPC")) 
                {                
                    if (collider.TryGetComponent(out NPC npc))
                    {
                        if (npc.IsRoaming && !npc.Followed)
                        {
                            npc.Followed = true;
                            Debug.Log(npc.gameObject.transform.position);
                            OnAddedNPC?.Invoke(this.gameObject, leaderTail);
                            followers++;
                            Debug.Log(followers);
                        }
                    }
                }              
            }    
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
