using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;



public class PhotonVS : MonoBehaviourPunCallbacks
{
   public override void OnConnectedToMaster()
    {
        CustomEvent.Trigger(gameObject, "OnConnectedToMaster");

    }
    public override void OnJoinedRoom()
    {
        CustomEvent.Trigger(gameObject, "OnJoinedRoom");
    }
public void SetUserName(string UserName)
{
PhotonNetwork.NickName = UserName;
}

    
}

