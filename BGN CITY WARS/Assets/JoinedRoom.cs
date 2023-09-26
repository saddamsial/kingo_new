using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


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

    }
}
