using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetWorkMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject DisconnectedIcon;


    private void Start()
    {
        PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.ConnectUsingSettings();
    }



    public void ReconnectToServer()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Invoke("ReconnectToServer", 1f);

        }
    }





    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server Successfully..");
        DisconnectedIcon.SetActive(false);
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected From NetWork.."+cause);
        DisconnectedIcon.SetActive(true);
        Invoke("ReconnectToServer", 1f);
    }
}



