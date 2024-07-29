using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Controls")]
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 10f;
    [Range(0f, 1f)][SerializeField] float stopFactor = 0.5f;
    [SerializeField] Transform feetPos;
    [SerializeField] Vector3 groundCheckSize = new Vector3(1, 0.05f, 1);
    [SerializeField] LayerMask whatIsGround;

    [Header("Better Platformer")]
    [SerializeField] private float hangTime = 0.1f;
    private float hangTimeCtr = 0;
    [SerializeField] private float jumpBufferLength = 0.1f;
    private float jumpBufferCtr = 0;

    // Private variables
    private Rigidbody2D rb;
    private float movement;
    private bool isGrounded = false;
    private bool canJump = false;
    private bool isUpsideDown = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void GetInput()
    {
        movement = Input.GetAxisRaw("Horizontal");

        // Check if grounded by a physics check
        isGrounded = Physics2D.OverlapBox(feetPos.position, groundCheckSize, 0f, whatIsGround);

        // Hang/Coyote Time
        if (isGrounded) { hangTimeCtr = hangTime; }
        else hangTimeCtr -= Time.deltaTime;

        // Jump Buffer
        if (Input.GetButtonDown("Jump")) { jumpBufferCtr = jumpBufferLength; }
        else jumpBufferCtr -= Time.deltaTime;

        // Jump Detection
        if (hangTimeCtr > 0f && jumpBufferCtr > 0)
        {
            canJump = true;
            jumpBufferCtr = 0;
        }

        // Controlled Jump
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * stopFactor);
        }

        // Inversion
        if (Input.GetKeyDown(KeyCode.F))
        {
            InvertPlayer();
        }
    }

    void InvertPlayer()
    {
        isUpsideDown = !isUpsideDown;

        if (isUpsideDown)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            rb.gravityScale *= -1; // Invert gravity
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.gravityScale *= -1; // Restore gravity
        }
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        var vel = rb.velocity;
        vel.x = movement * speed;
        rb.velocity = vel;

        if (canJump)
        {
            rb.AddForce(Vector2.up * jumpForce * (isUpsideDown ? -1 : 1), ForceMode2D.Impulse);
            canJump = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(feetPos.position, groundCheckSize);
    }
}
