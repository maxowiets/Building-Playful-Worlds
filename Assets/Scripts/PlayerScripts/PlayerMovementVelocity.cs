using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementVelocity : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;
    public float jumpHeight;
    public float gravityStrength;
    public float drag;

    float forward;
    float backward;
    float right;
    float left;
    float sprint;
    bool jump;
    bool jumped;

    public Rigidbody rb;

    Vector3 groundCheck = new Vector3(0, -0.51f, 0);
    float groundDistance = 0.5f;
    public LayerMask groundLayer;

    void FixedUpdate()
    {
        //drag
        rb.velocity = new Vector3(rb.velocity.x * (1 - drag * Time.fixedDeltaTime), rb.velocity.y, rb.velocity.z * (1 - drag * Time.fixedDeltaTime));

        //movement input
        if (Input.GetKey(KeyCode.W)) forward = 1.0f; else forward = 0.0f;
        if (Input.GetKey(KeyCode.S)) backward = 1.0f; else backward = 0.0f;
        if (Input.GetKey(KeyCode.A)) left = 1.0f; else left = 0.0f;
        if (Input.GetKey(KeyCode.D)) right = 1.0f; else right = 0.0f;
        if (Input.GetKey(KeyCode.LeftShift)) sprint = sprintSpeed * PlayerStats.SprintSpeedMultiplier; else sprint = 1f;

        //movement calculation
        float totalSpeed = speed * PlayerStats.SpeedMultiplier * sprint;
        Vector3 fw = transform.forward;
        float forwardSpeed = rb.velocity.z + ((forward - backward) * fw.z + (left - right) * fw.x) * totalSpeed * 10 * Time.fixedDeltaTime;
        float sidewaysSpeed = rb.velocity.x + ((forward - backward) * fw.x + (right - left) * fw.z) * totalSpeed * 10 * Time.fixedDeltaTime;

        Vector2 speedCombiner = new Vector2(sidewaysSpeed, forwardSpeed);
        if (speedCombiner.magnitude > totalSpeed)
        {
            speedCombiner = new Vector2(sidewaysSpeed, forwardSpeed).normalized * totalSpeed;
        }
        rb.velocity = new Vector3(speedCombiner.x, rb.velocity.y, speedCombiner.y);

        //jump input and forces
        if (jumped && rb.velocity.y <= 0) jumped = false;

        jump = Input.GetKey(KeyCode.Space);
        if (jump && IsGrounded() && !jumped)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpHeight * PlayerStats.JumpHeightMultiplier* 2f * -gravityStrength), rb.velocity.z);
            jumped = true;
        }

        //gravity forces
        if (!IsGrounded()) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + (gravityStrength * Time.fixedDeltaTime), rb.velocity.z);


    }

    //ground check
    bool IsGrounded()
    {
        bool grounded = Physics.CheckSphere(transform.position + groundCheck, groundDistance, groundLayer);
        return grounded;
    }
}
