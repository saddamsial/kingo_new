
using UnityEngine;
using Photon.Pun;
public class PlayerScores : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
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
        if (stream.IsWriting) // is Writing(CurrentPlayer)
        {
            Debug.Log("before stream.SendNext(PlayerName +  stream.SendNext(TotalRoomKills");
            stream.SendNext(PlayerName);
            stream.SendNext(TotalRoomKills);   //room kills Track
            Debug.Log("after stream.SendNext(PlayerName +  stream.SendNext(TotalRoomKills");
        }
        else if  (stream.IsReading) // is Reading(otherplayers)
        { 
            Debug.Log("before TotalRoomKills = (int)stream.ReceiveNext" + "PlayerName = (string)stream.ReceiveNext");
            PlayerName = (string)stream.ReceiveNext();
            TotalRoomKills = (int)stream.ReceiveNext(); // total room kills
            Debug.Log("TotalRoomKills = (int)stream.ReceiveNext" + "PlayerName = (string)stream.ReceiveNext");
        }

    }

}
