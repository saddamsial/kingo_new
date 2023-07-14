using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;



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
    }
public void SetUserName(string UserName)
{
PhotonNetwork.NickName = UserName;
}

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //base.OnRoomListUpdate(roomList);
        UpdateRoomList(roomList);
    
    }
   void UpdateRoomList(List<RoomInfo>list)
   {
   foreach(RoomItem item in roomitemlist)
   {
    Destroy(item.gameObject);
   }
    roomitemlist.Clear();

   foreach(RoomInfo room in list)
   {
    RoomItem newRoom = Instantiate(RoomItemPrefab,ContentObject);
    newRoom.SetRoomName(room.Name);
    roomitemlist.Add(newRoom);
   }


   }
    
}

