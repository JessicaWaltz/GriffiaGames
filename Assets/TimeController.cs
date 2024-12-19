using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float startTime = 60f; // Set your desired starting time here
    private Text timerText;

    private float currentTime;

    void Start()
    {
        timerText = GetComponent<Text>();
        currentTime = startTime;
        UpdateTimerText();
    }

    void Update()
    {
        // Check if the timer has reached 0
        if (currentTime <= 0f)
        {
            // Timer has reached 0, you can perform actions here
            Debug.Log("Time's up!");
            return;
        }

        // Update the timer
        currentTime -= Time.deltaTime;
        if (currentTime < 0) {
            currentTime = 0;
        }
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        // Format the time as minutes:seconds
        string minutes = Mathf.Floor(currentTime / 60).ToString("0");
        string seconds = (currentTime % 60).ToString("00");

        // Update the UI text
        timerText.text = minutes + ":" + seconds;
    }
}
