using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float acceleration;

    float forward;
    float backward;
    float right;
    float left;
    float sprint;

    public CharacterController controller;
    public float gravity;
    public Vector3 gravityDirection;

    public Vector3 groundCheck;
    public float groundDistance;
    public LayerMask groundLayer;
    public float jumpHeight;


    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //WASD input for the movement
        if (Input.GetKey(KeyCode.W)) if (forward <= 1) forward += acceleration * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) if (backward <= 1) backward += acceleration * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) if (left <= 1) left += acceleration * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) if (right <= 1) right += acceleration * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift)) sprint = 1.6f; else sprint = 1f;

        if (forward > 0) forward -= .5f * acceleration * Time.deltaTime;
        if (backward > 0) backward -= .5f * acceleration * Time.deltaTime;
        if (left > 0) left -= .5f * acceleration * Time.deltaTime;
        if (right > 0) right -= .5f * acceleration * Time.deltaTime;

        Vector3 move = (right - left) * transform.right + (forward * sprint - backward) * transform.forward;

        //GroundedCheck
        if (isGrounded()) gravityDirection.y = 0;
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
        }
        gravityDirection.y -= gravity * Time.deltaTime;

        controller.Move(gravityDirection.y * transform.up * Time.deltaTime + move * speed * Time.deltaTime);
    }

    bool isGrounded()
    {
        bool grounded = Physics.CheckSphere(transform.position + groundCheck, groundDistance, groundLayer);
        return grounded;

    }

    void Jump()
    {
        gravityDirection.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
    }
}
