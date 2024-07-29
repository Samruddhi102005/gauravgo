using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorControl : MonoBehaviour
{
    public float speed = 2.0f;
    public float upperLimit = 5.0f;
    public float lowerLimit = -5.0f;

    private bool movingUp = true;

    void Update()
    {
        // Move the elevator up or down
        if (movingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (transform.position.y >= upperLimit)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= lowerLimit)
            {
                movingUp = true;
            }
        }
    }
}
