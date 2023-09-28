using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RPCCALLS : MonoBehaviourPunCallbacks
{ 
    public Transform JoinedRoomMessage;
    public Transform leftRoomMessage;


   
    [PunRPC]
    public void newPlayerJoined()
    {
        JoinedRoomMessage.gameObject.SetActive(true);

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        JoinedRoomMessage.gameObject.SetActive(true);
    JoinedRoomMessage.GetComponent<Text>().text = newPlayer.NickName+ (" ")+JoinedRoomMessage.transform.name;
    }

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        leftRoomMessage.gameObject.SetActive(true);
        leftRoomMessage.GetComponent<Text>().text = newPlayer.NickName + (" ") + leftRoomMessage.transform.name;
    }













}




