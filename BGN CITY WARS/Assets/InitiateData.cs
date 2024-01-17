using UnityEngine;
using Photon.Pun;
public class InitiateData : MonoBehaviour
{
    [SerializeField]
    private  int NewPlayer;
    [SerializeField]
    private int DefaultCoins;
    [SerializeField]
    private string PlayerName;

    private void OnEnable()
    {
        PhotonNetwork.NickName = PlayerPrefs.GetString("UserStats_PlayerName");
    }
    void Start()
    {
        PlayerPrefs.SetInt("NewPlayer", PlayerPrefs.GetInt("NewPlayer"));
        // data initiatation.
      if (PlayerPrefs.GetInt ("FirstTime") ==0)
        {
            int RandomNums = Random.Range(1000,10000);

            //set all initiatated data.
            PlayerPrefs.SetString("PlayerName", "Player" + RandomNums.ToString());

            PlayerPrefs.SetInt("BCoins", DefaultCoins);

            PlayerPrefs.SetInt("LVL", 1);

            PlayerPrefs.SetInt("XP", 0);

            PlayerPrefs.SetInt("FirstTime", 1);
        }
     else
     // data retrieving.
        {
            //  PlayerPrefs.GetString("PlayerName", PlayerName);
            int RandomNums = Random.Range(1000, 10000);
            PlayerPrefs.SetString("PlayerName", PlayerName + RandomNums.ToString());
            PlayerPrefs.SetInt("BCoins", PlayerPrefs.GetInt("BCoins"));

            PlayerPrefs.SetInt("LVL", PlayerPrefs.GetInt("LVL"));

            PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP"));
        }
    }

    public void PrintTest()
    {
        Debug.Log("Printed UserStats_PlayerName " + PlayerPrefs.GetString("UserStats_PlayerName"));
        Debug.Log("Printed LVL " + PlayerPrefs.GetInt("LVL"));
        Debug.Log("Printed FirstTime " + PlayerPrefs.GetInt("FirstTime"));
    }

}
