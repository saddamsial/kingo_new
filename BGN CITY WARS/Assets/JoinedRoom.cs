using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class JoinedRoom : MonoBehaviourPunCallbacks
{
    public string RoomSceneName;
    public Transform LoadingScreen;
    [SerializeField]
    private int MaxroomsAllowed;
    [SerializeField]
    private byte MaxPlayersinRoom;
    [SerializeField]
    private Transform ErrorMaxRooms;

    [SerializeField]
    private Transform ErrorRoomFull;


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
    public void CreateOrJoinroom()
    {
        if (PhotonNetwork.CountOfRooms < MaxroomsAllowed)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MaxPlayersinRoom; // Set the maximum number of players for the room

            PhotonNetwork.JoinOrCreateRoom("RoomSceneName", roomOptions, TypedLobby.Default);

        }
        else
            ErrorMaxRooms.gameObject.SetActive(true);
    }
}
