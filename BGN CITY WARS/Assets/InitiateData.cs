using UnityEngine;
using Photon.Pun;
public class InitiateData : MonoBehaviour
{
  
    [SerializeField]
    private int DefaultCoins;


    private void OnEnable()
    {
        PhotonNetwork.NickName = PlayerPrefs.GetString("UserStats_PlayerName");
    }
    void Start()
    {
        // data initiatation.
      if (PlayerPrefs.GetInt ("FirstTime") ==0)
        {
            int RandomNums = Random.Range(1000,10000);

            //set all initiatated data.
            #region Initiate UserStats
            PlayerPrefs.SetString("UserStats_PlayerName", "Player" + RandomNums.ToString());

            PlayerPrefs.SetInt("BCoins", DefaultCoins);

            PlayerPrefs.SetInt("LVL", 1);

            PlayerPrefs.SetInt("XP", 0);
            #endregion

            #region GameSettings
            //Audio
            PlayerPrefs.SetFloat("Settings_Music Volume", 0.5f);
            PlayerPrefs.SetFloat("Settings_SFX Volume", .75f);

            //Camera
            PlayerPrefs.SetInt("Settings_CamMode", 0);
            PlayerPrefs.SetFloat("Settings_GeneralSens", 0.25f);
            PlayerPrefs.SetFloat("Settings_ScopeSens", 0.15f);


            #endregion

            PlayerPrefs.SetInt("FirstTime",1);

            #region NetWorking
            PhotonNetwork.NickName = PlayerPrefs.GetString("UserStats_PlayerName");
            #endregion
        }
        else
     // data retrieving.
        {
            #region Recieve UserStats
            int RandomNums = Random.Range(10000, 100000);
            PlayerPrefs.SetString("UserStats_PlayerName", PlayerPrefs.GetString("UserStats_PlayerName"));

            PlayerPrefs.SetInt("BCoins", PlayerPrefs.GetInt("BCoins"));

            PlayerPrefs.SetInt("LVL", PlayerPrefs.GetInt("LVL"));

            PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP"));
            #endregion

            PlayerPrefs.SetInt("FirstTime", PlayerPrefs.GetInt("FirstTime"));
        }
    }

    public void PrintTest(string key)
    {
        Debug.Log(PlayerPrefs.GetString(key));

        Debug.Log(PlayerPrefs.GetInt(key));
        Debug.Log(PlayerPrefs.GetFloat(key));

    }

    }