using UnityEngine;
using Photon.Pun;   


public class PhotonSerializerBGN : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField]
    private new PhotonView photonView;
   

  [Header("Skin")]
    public string SkinID;
    [Header("Weapon")]
    public int InventoryTrack;
    public int CurrentWeaponType;

    [Header("IK")]
    public int LookIK;
    public int AimIK;



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

          stream.SendNext(CurrentWeaponType);    //SelectedWeaponTypeTrackerSync

            stream.SendNext(LookIK);    //lookIK

            stream.SendNext(AimIK);    //AimIK

        }
        else
        {
            SkinID= (string)stream.ReceiveNext();  //other player SkinID

            InventoryTrack = (int)stream.ReceiveNext(); // other player Inventory

            CurrentWeaponType = (int)stream.ReceiveNext(); //other player CurrentWeaponType

            LookIK = (int)stream.ReceiveNext(); // other player LookIK

            AimIK = (int)stream.ReceiveNext(); // other player aIMik


        }
    }


    










   

}
