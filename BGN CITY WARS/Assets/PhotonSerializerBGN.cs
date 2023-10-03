using UnityEngine;
using Photon.Pun;   


public class PhotonSerializerBGN : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField]
    private new PhotonView photonView;
    [Header("Player Status")]
    public int HP;
    public int Shield;
    public int Stamina;
   

    [Header("Skin")]
    public string SkinID;

    [Header("Weapon")]
    public int InventoryTrack;
    public int CurrentWeaponType;

    [Header("IK")]
    public int LookIK;
    public int AimIK;

    [Header("Scores")]
    public int TotalRoomKills;
    public int TotalGameKills;
    public int TotalRoomDeaths;
    public int TotalGameDeaths;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("streamIsCalled");
        if (stream.IsWriting && photonView != null)
        {
            stream.SendNext(HP); //HPSync

            stream.SendNext(Shield); //ShieldSync

            stream.SendNext(SkinID); //SkinSync
        
            stream.SendNext(InventoryTrack);    //InventoryTrackerSync

          stream.SendNext(CurrentWeaponType);    //SelectedWeaponTypeTrackerSync

            stream.SendNext(LookIK);    //lookIK

            stream.SendNext(AimIK);    //AimIK

            stream.SendNext(TotalRoomKills);   //room kills Track


        }
        else
        {
            HP = (int)stream.ReceiveNext(); // other player HP

            Shield = (int)stream.ReceiveNext(); // other player Shield

            SkinID = (string)stream.ReceiveNext();  //other player SkinID

            InventoryTrack = (int)stream.ReceiveNext(); // other player Inventory

            CurrentWeaponType = (int)stream.ReceiveNext(); //other player CurrentWeaponType

            LookIK = (int)stream.ReceiveNext(); // other player LookIK

            AimIK = (int)stream.ReceiveNext(); // other player aIMik

            TotalRoomKills = (int)stream.ReceiveNext(); // total room kills

        }
    }


    










   

}
