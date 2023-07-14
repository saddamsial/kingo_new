using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomItem : MonoBehaviour
{
    public Text roomName;
    public Text roomPlayers;
    public Text roomMaxPlayers;
    PhotonVS Manager;

    private void Start()
    {
        Manager = FindObjectOfType<PhotonVS>();
    }

    public void SetRoomName(string _RoomName, int currentPlayers, int maxPlayers)
    {
        roomName.text = _RoomName;
        roomPlayers.text = currentPlayers.ToString();
        roomMaxPlayers.text = maxPlayers.ToString();
    }



public void OnClickItem()
{
    Manager.JoinRoom(roomName.text);
}


}
