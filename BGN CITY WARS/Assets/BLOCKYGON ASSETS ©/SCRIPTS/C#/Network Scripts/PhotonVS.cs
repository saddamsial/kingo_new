using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using Photon;



public class PhotonVS : MonoBehaviourPunCallbacks
{
    public RoomItem RoomItemPrefab;
    List<RoomItem>roomitemlist = new List<RoomItem>();
    public Transform ContentObject;
   public override void OnConnectedToMaster()
    {
        CustomEvent.Trigger(gameObject, "OnConnectedToMaster");

    }
    public override void OnJoinedRoom()
    {
        CustomEvent.Trigger(gameObject, "OnJoinedRoom");
        Debug.Log("joinedRoom Successfully");
    }
public void SetUserName(string UserName)
{
PhotonNetwork.NickName = UserName;
}

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //base.OnRoomListUpdate(roomList);
        UpdateRoomList(roomList);
        Debug.Log("override");
    
    }
  void UpdateRoomList(List<RoomInfo> list)
{
    foreach (RoomItem item in roomitemlist)
    {
        Destroy(item.gameObject);
    }

    roomitemlist.Clear();
    Debug.Log("clear");

    foreach (RoomInfo room in list)
    {
        RoomItem newRoom = Instantiate(RoomItemPrefab, ContentObject);
        Debug.Log("instantiate");
        newRoom.SetRoomName(room.Name, room.PlayerCount, room.MaxPlayers);
        roomitemlist.Add(newRoom);
    }
}

   public void Refreshobby() 
{
   PhotonNetwork.JoinLobby();  
}



    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the lobby!");
        // Additional actions after successfully joining the lobby
    }


public void JoinRoom(string roomName)
{
PhotonNetwork.JoinRoom(roomName);
}



   



}

