using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isUpsideDown = false;
    private bool isGravityInverted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Check if the 'F' key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the gravity scale
            isGravityInverted = !isGravityInverted;
            rb.gravityScale = isGravityInverted ? -1 : 1;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (rb.gravityScale > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Jump with positive gravity
            }
            else if (rb.gravityScale < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, -jumpForce); // Jump with inverted gravity
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }



}