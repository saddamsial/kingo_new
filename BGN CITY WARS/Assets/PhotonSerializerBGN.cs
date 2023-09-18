using UnityEngine;
using Photon.Pun;   


public class PhotonSerializerBGN : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField]
    private new PhotonView photonView;
   
  
    public string SkinID;
    public int InventoryTrack;
    public int CurrentWeaponType;

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

            stream.SendNext(InventoryTrack);    //SelectedWeaponTypeTrackerSync


        }
        else
        {
            SkinID= (string)stream.ReceiveNext();  //other player SkinID

            InventoryTrack = (int)stream.ReceiveNext(); // other player Inventory

            CurrentWeaponType = (int)stream.ReceiveNext(); //other player CurrentWeaponType


        }
    }


    










   

}
