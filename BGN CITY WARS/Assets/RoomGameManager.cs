using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class RoomGameManager : MonoBehaviour
{
 [Header("GameWinner")]
    public GameObject CurrentWinnner;
    public GameObject LastRoundWinner;
    [Header("GameTimeSettings")]
    public Text timerText;
    public float RoundTime = 60.0f; // Set your countdown time in seconds here
    private float currentTime;

    void Start()
    {
        currentTime = RoundTime;
    }

    void Update()
    {
        // Update the timer
        currentTime -= Time.deltaTime;

        // Display the time as minutes and seconds
        float minutes = Mathf.Floor(currentTime / 60);
        float seconds = Mathf.RoundToInt(currentTime % 60);

        // Update the UI text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Check if the countdown has reached zero
        if (currentTime <= 0)
        {
            // Timer has reached zero, you can perform actions here
            timerText.text = "Time's Up!";
            // Optionally, you can stop the countdown or trigger other events.
        }
    }
}