using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabcontroller : MonoBehaviour
{
    public Transform grabDetect;
    public Transform boxHolder;
    public float rayDist;
    private GameObject grabbedObject;

    private void Update()
    {
        RaycastHit2D grabcheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale.x, rayDist);

        if (grabcheck.collider != null && grabcheck.collider.CompareTag("box"))
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (grabbedObject == null)
                {
                    // Grab the object
                    grabbedObject = grabcheck.collider.gameObject;
                    grabbedObject.transform.SetParent(boxHolder);
                    grabbedObject.transform.localPosition = Vector3.zero; // Adjust the position relative to the holder
                    grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                }
                else
                {
                    // Release the object
                    grabbedObject.transform.SetParent(null);
                    grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    grabbedObject = null;
                }
            }
        }
        else if (grabbedObject != null && Input.GetKeyDown(KeyCode.G))
        {
            // Release the object if the player presses 'G' again when there's no box in front
            grabbedObject.transform.SetParent(null);
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject = null;
        }

        if (grabbedObject != null && Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the gravity of the grabbed object
            Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = -rb.gravityScale;
        }

        Debug.DrawRay(grabDetect.position, Vector2.right * transform.localScale.x * rayDist, Color.red);
    }
}
