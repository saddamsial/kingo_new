using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class PunCallBacks : MonoBehaviourPunCallbacks
{
    public string VSJoinedLobby;
    public string VSJoinedRoom;
    public GameObject GameObj;
    public override void OnJoinedLobby()
    {
        CustomEvent.Trigger(GameObj, VSJoinedLobby);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom   CustomEvent.Trigger(GameObj,VSJoinedRoom);");
        CustomEvent.Trigger(GameObj,VSJoinedRoom);
        Debug.Log("OnJoinedRoom   CustomEvent.Trigger(GameObj,VSJoinedRoom);");
    }

        

}
