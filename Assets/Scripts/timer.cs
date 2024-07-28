using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public float currentTime = 0f;
    public float timerTime = 0f;

    [SerializeField] Text CountdownText;

    private void Start()
    {
        currentTime = timerTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        CountdownText.text = currentTime.ToString("0");

        CountdownText.color = Color.yellow;

        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }

}
