using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementForce : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;
    public float jumpHeight;
    public float gravityStrength;
    public float drag;

    bool forward;
    bool backward;
    bool right;
    bool left;
    float sprint;
    bool jump;
    bool jumped;

    public Rigidbody rb;

    Vector3 groundCheck = new Vector3(0, -0.51f, 0);
    float groundDistance = 0.5f;
    public LayerMask groundLayer;

    void FixedUpdate()
    {
        //input
        forward = Input.GetKey(KeyCode.W);
        backward = Input.GetKey(KeyCode.S);
        right = Input.GetKey(KeyCode.D);
        left = Input.GetKey(KeyCode.A);
        if (Input.GetKey(KeyCode.LeftShift)) sprint = sprintSpeed; else sprint = 1;

        //input to forces
        if (forward) rb.AddForce(transform.forward * speed * sprint);
        if (backward) rb.AddForce(transform.forward * -speed * sprint);
        if (right) rb.AddForce(Quaternion.AngleAxis(90, Vector3.up) * transform.forward * speed * sprint);
        if (left) rb.AddForce(Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * speed * sprint);

        //jump input and forces
        if (jumped && rb.velocity.y <= 0) jumped = false;

        jump = Input.GetKey(KeyCode.Space);
        if (jump && IsGrounded() && !jumped) {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpHeight * 2f * -gravityStrength), rb.velocity.z);
            jumped = true;
        }

        //gravity forces
        if (!IsGrounded()) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + (gravityStrength * Time.deltaTime), rb.velocity.z);

        //drag
        rb.velocity = new Vector3(rb.velocity.x * (1 - drag * Time.deltaTime), rb.velocity.y, rb.velocity.z * (1 - drag * Time.deltaTime));
    }

    //ground check
    bool IsGrounded()
    {
        bool grounded = Physics.CheckSphere(transform.position + groundCheck, groundDistance, groundLayer);
        return grounded;
    }

}