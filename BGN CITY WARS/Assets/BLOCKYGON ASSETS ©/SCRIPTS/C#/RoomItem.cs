using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomItem : MonoBehaviour
{
public Text roomName;
PhotonVS Manager;

private void Start() 
{
   Manager = FindObjectOfType<PhotonVS>();
}
public void SetRoomName(string _RoomName)
{
 roomName.text=_RoomName;
}

public void OnClickItem()
{
    Manager.JoinRoom(roomName.text);
}


}
