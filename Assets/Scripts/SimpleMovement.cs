using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;
    private float moveHorizontal;
    private float moveVertical;
    
    private Vector3 moveDirection;
    private Vector3 velocity;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
        moveDirection = transform.TransformDirection(moveDirection);
        velocity = moveDirection * speed;
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetMouseButton(0))
            SetTargetPosition();
    }

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            targetPosition = hit.point;
            this.transform.LookAt(targetPosition);
        }
    }

    private void PlayerInput()
    {
        this.moveHorizontal = Input.GetAxis("Horizontal");
        this.moveVertical = Input.GetAxis("Vertical");
    }
}
