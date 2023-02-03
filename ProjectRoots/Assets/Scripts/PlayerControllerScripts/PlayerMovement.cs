using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask WhatIsGround;
    bool grounded;

    public Transform orientation;
    //private TextMeshProUGUI speedText; CAN BE REMOVED

    float horInput;
    float vertInput;

    Vector3 moveDir;

    Rigidbody rb;

    private void Start()
    {
        //speedText = GameObject.Find("SpeedText").GetComponent<TextMeshProUGUI>(); CAN BE REMOVED

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        grounded = true;
    }
    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, WhatIsGround);

        MoveInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MoveInput()
    {
        // Get horizontal and vertical inputs
        horInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        //jump
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }



    }

    private void MovePlayer()
    {
        // calculate move direction to walk in the direction the player is facing 
        moveDir = orientation.forward * vertInput + orientation.right * horInput;

        // on ground
        if (grounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }


        // limit stop speed
        if (rb.velocity.magnitude < 0.001)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        //speedText.text = "Speed: " + rb.velocity.magnitude; CAN BE REMOVED
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}