using UnityEngine;
using Photon.Pun;   


public class PhotonSerializerBGN : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField]
    private new PhotonView photonView;
   
  
    public string SkinID;
    public int InventoryTrack;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("streamIsCalled");
        if (stream.IsWriting && photonView != null)
        {
            
            stream.SendNext(SkinID); //SkinSync
        
            stream.SendNext(InventoryTrack);    //InventoryTrackerSync

        }
        else
        {
            SkinID= (string)stream.ReceiveNext();
            InventoryTrack = (int)stream.ReceiveNext();//SkinSync


        }
    }


    










   

}
