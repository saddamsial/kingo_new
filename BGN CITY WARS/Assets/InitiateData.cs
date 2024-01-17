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
            PlayerPrefs.SetFloat("Music Volume", 0.5f);
            PlayerPrefs.SetFloat("SFX Volume", .75f);

            //Camera
            PlayerPrefs.SetInt("CamMode", 0);
            PlayerPrefs.SetFloat("GeneralSens", 0.25f);
            PlayerPrefs.SetFloat("ScopeSens", 0.15f);


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

    public void PrintTest()
    {
        Debug.Log("Printed UserStats_PlayerName " + PlayerPrefs.GetString("UserStats_PlayerName"));
        Debug.Log("Printed LVL " + PlayerPrefs.GetInt("LVL"));
        Debug.Log("Printed FirstTime " + PlayerPrefs.GetInt("FirstTime"));
    }





}
