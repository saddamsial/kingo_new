using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;



public class RoomGameManager : MonoBehaviour,IPunObservable
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
        // if (PhotonNetwork.IsMasterClient)
        //   { 
        // Update the timer
        currentTime -= Time.deltaTime;
        //  }
        // Display the time as minutes and seconds
        float minutes = Mathf.Floor(currentTime / 60);
        float seconds = Mathf.RoundToInt(currentTime % 60);


        // Update the UI text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Check if the countdown has reached zero
        if (currentTime <= 0)
        {
            // Timer has reached zero, you can perform actions here

            if (currentTime <= 0)
            {
                Players = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerScoreItem"));
                int highestKills = 0;
                GameObject playerWithHighestKills = null;
                bool hasEqualKills = false; // Flag to track if any two players have equal kills

                foreach (GameObject player in Players)
                {
                    int playerKills = player.GetComponent<PlayerScores>().TotalRoomKills;

                    if (playerKills > highestKills)
                    {
                        highestKills = playerKills;
                        playerWithHighestKills = player;
                        hasEqualKills = false; // Reset the flag when a higher kill count is found.
                    }
                    else if (playerKills == highestKills)
                    {
                        hasEqualKills = true; // Two players have equal kills.
                    }
                }

                if (hasEqualKills)
                {
                    CurrentWinnner = null; // Set the winner to null if any two players have equal kills.
                }
                else
                {
                    CurrentWinnner = playerWithHighestKills; // Set the player with the highest kills as the winner.
                }

                WinnerAnnounceUI.SetActive(true);
                currentTime = RoundTime;
            }
        }
    }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
      if(PhotonNetwork.IsMasterClient)
        {
            stream.SendNext(currentTime);
        }
      else
        {
            currentTime = (float)stream.ReceiveNext();
        }
    }
}