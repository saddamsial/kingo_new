using UnityEngine;
using Photon.Pun;   


public class PhotonSerializerBGN : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField]
    private new PhotonView photonView;

    public string SkinID;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("streamIsCalled");
        if (stream.IsWriting && photonView != null)
        {
            Debug.Log("streamIsWriting"+ SkinID);
                stream.SendNext(SkinID);
        }
        else
        {
            SkinID= (string)stream.ReceiveNext();
            Debug.Log("streamIsReading" + SkinID);

        }
    }


    
}
