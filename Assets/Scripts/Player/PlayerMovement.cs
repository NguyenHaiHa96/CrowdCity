using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkForwardSpeed;
    [SerializeField] private float walkBackwardSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float gravity;    
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float dampTime;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] private Animator animator;
    [SerializeField] Rigidbody rb;
    [SerializeField] private CharacterController controller;

    private Vector3 moveDirection;
    private Vector3 velocity;   
    private float moveHorizontal;
    private float moveVertical;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
        //if (Input.GetKeyDown(KeyCode.Mouse0))
            //StartCoroutine(Attack());
    }

    private void CheckGrounded()
    {
        if (Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask))
            isGrounded = true;
        else
            isGrounded = false;
        animator.SetBool("Jump", !isGrounded);  
    }

    private void Move()
    {
        CheckGrounded();
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
        moveDirection = transform.TransformDirection(moveDirection);

        if (isGrounded)
        {
            if (moveDirection != Vector3.zero)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    Run();
                else
                    WalkForward();
            }
            else if (moveDirection == Vector3.zero)
                Idle();
            velocity = moveDirection * moveSpeed;
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }          
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        animator.SetFloat("Horizontal", moveHorizontal);
        animator.SetFloat("Vertical", moveVertical);
    }

    private void WalkBackward()
    {
        moveSpeed = walkBackwardSpeed;
    }

    private void Idle()
    {
        moveSpeed = 0;
        //animator.SetFloat("Horizontal", moveHorizontal, dampTime, Time.deltaTime);
        //animator.SetFloat("Vertical", moveVertical, dampTime, Time.deltaTime);
    }

    private void WalkForward()
    {
        Debug.Log(moveHorizontal);
        moveSpeed = moveHorizontal;
        //animator.SetFloat("Horizontal", moveHorizontal, dampTime, Time.deltaTime);
        //animator.SetFloat("Vertical", moveVertical, dampTime, Time.deltaTime);
        //animator.SetFloat("Speed", 0.5f, dampTime, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        //animator.SetFloat("Horizontal", moveHorizontal, dampTime, Time.deltaTime);
        //animator.SetFloat("Vertical", moveVertical, dampTime, Time.deltaTime);
        //animator.SetFloat("Speed", 1f, dampTime, Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -gravity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckDistance);
    }

    private IEnumerator Attack()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.9f);
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
    }
}
