using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name + " with tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("Collided with Spike");
            Die();
        }
    }


    void Die()
    {

        transform.position = new Vector3(-15f, -5f, 0f);

        rb.velocity = Vector2.zero;


        // Reset gravity and other player states if needed
      //  playerMovement.isGravityInverted = false;
        rb.gravityScale = 1;

    }


}
