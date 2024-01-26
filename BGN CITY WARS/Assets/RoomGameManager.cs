using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomGameManager : MonoBehaviour, IPunObservable
{
    [Header("GameWinner")]
    public GameObject CurrentWinnner;
    public GameObject LastRoundWinner;

    [Header("GameTimeSettings")]
    public TextMeshProUGUI timerText;
    public float RoundTime = 60.0f; // Set your countdown time in seconds here
    private float currentTime;

    [SerializeField]
    private Transform LoadingScreen;

    public GameObject WinnerAnnounceUI;
    public  List<GameObject> Players;

    private float previousSeconds = -1;

    void Start()
    {
        currentTime = RoundTime;
        Players = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerScoreItem"));
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        float seconds = Mathf.RoundToInt(currentTime % 60);

        if (seconds != previousSeconds)
        {
            float minutes = Mathf.Floor(currentTime / 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            previousSeconds = seconds;
        }

        if (currentTime <= 0)
        {
            EvaluateWinner();
        }
    }

    void EvaluateWinner()
    {
        Players = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerScoreItem"));
        int highestKills = 0;
        GameObject playerWithHighestKills = null;
        bool hasEqualKills = false;

        foreach (GameObject player in Players)
        {
            PlayerScores playerScores = player.GetComponent<PlayerScores>();
            int playerKills = playerScores.TotalRoomKills;

            if (playerKills > highestKills)
            {
                highestKills = playerKills;
                playerWithHighestKills = player;
                hasEqualKills = false;
            }
            else if (playerKills == highestKills)
            {
                hasEqualKills = true;
            }
        }

        if (hasEqualKills || Players.Count < 2)
        {
            CurrentWinnner = null;
        }
        else
        {
            CurrentWinnner = playerWithHighestKills;
        }

        WinnerAnnounceUI.SetActive(true);
        currentTime = RoundTime;

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            stream.SendNext(currentTime);
        }
        else
        {
            currentTime = (float)stream.ReceiveNext();
        }
    }

    public void BacktoMainMenu()
    {
        LoadingScreen.gameObject.SetActive(true);
        SceneManager.LoadScene("MAIN MENU");
    }
}
