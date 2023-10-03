using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerScores : MonoBehaviourPunCallbacks,IPunObservable
{
    private new PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    [Header("Player Room Status")]
    public string PlayerName;
    public int TotalRoomKills;
    public int TotalRoomDeaths;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && photonView != null) // is Writing(CurrentPlayer)
        {
            stream.SendNext(PlayerName);
            stream.SendNext(TotalRoomKills);   //room kills Track
        }
       else // is Reading(otherplayers)
        {
            TotalRoomKills = (int)stream.ReceiveNext(); // total room kills
            PlayerName = (string)stream.ReceiveNext();
        }
    }

}
