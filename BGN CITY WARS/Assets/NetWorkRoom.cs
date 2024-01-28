using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetWorkRoom : MonoBehaviourPunCallbacks

{
    public GameObject IconDisconnect;

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause);
       // IconDisconnect.SetActive(true);
        SceneManager.LoadScene("MAIN MENU");
    }


    public override void OnConnected()
    {
        Debug.Log("Connected..");
   //     IconDisconnect.SetActive(false);
    }

}
