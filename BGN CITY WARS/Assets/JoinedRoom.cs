using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class JoinedRoom : MonoBehaviourPunCallbacks
{
    public string RoomSceneName;
    public Transform LoadingScreen;

   
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(RoomSceneName);
        if(LoadingScreen!=null)
        {
            LoadingScreen.gameObject.SetActive(true);
        }

        PhotonView pv = GetComponent<PhotonView>();
   
        pv.RPC("newPlayerJoined", RpcTarget.All);
        
    }
    public void LoadRoom()
    {

    }
}
