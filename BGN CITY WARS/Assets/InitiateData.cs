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
        // data initiatation.
     if ( PlayerPrefs.GetInt (" NewPlayer") ==0 )
        {
            int RandomNums = Random.Range(1000,10000);

            //set all initiatated data.
            PlayerPrefs.SetString("PlayerName", PlayerName + RandomNums.ToString());

            PlayerPrefs.SetInt("BCoins", DefaultCoins);

            PlayerPrefs.SetInt("LVL", 1);

            PlayerPrefs.SetInt("XP", 0);
        }
     else
     // data retrieving.
        {
            PlayerPrefs.GetString("PlayerName", PlayerName);

            PlayerPrefs.SetInt("BCoins", PlayerPrefs.GetInt("BCoins"));

            PlayerPrefs.SetInt("LVL", PlayerPrefs.GetInt("LVL"));

            PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP"));
        }
    }


}
