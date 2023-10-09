using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;



public class RoomGameManager : MonoBehaviour
{
 [Header("GameWinner")]
    public GameObject CurrentWinnner;
    public GameObject LastRoundWinner;
    [Header("GameTimeSettings")]
    public Text timerText;
    public float RoundTime = 60.0f; // Set your countdown time in seconds here
    private float currentTime;

    public GameObject WinnerAnnounceUI;
    public List<GameObject> Players;

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

            Players = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerScoreItem"));
            int highestKills = 0; // Initialize the variable to store the highest kills.
            GameObject playerWithHighestKills = null; // Initialize the variable to store the player with the highest kills.

            foreach (GameObject player in Players)
            {
                // Assuming PlayerActionsVar has a variable called "TotalRoomkillsTrack"
                int playerKills = player.GetComponent<PlayerScores>().TotalRoomKills;

                // Check if the current player has more kills than the current highest.
                if (playerKills > highestKills)
                {
                    highestKills = playerKills;
                    playerWithHighestKills = player;
                }
            }

            // Set the player with the highest kills to the CurrentWinnner variable.
            CurrentWinnner = playerWithHighestKills;



            WinnerAnnounceUI.SetActive(true);
            // Optionally, you can stop the countdown or trigger other events.
            currentTime = RoundTime;
        }
    }
}