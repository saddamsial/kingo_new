using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SerializeBool : MonoBehaviour,IPunObservable
{
    public bool Activate;
    private PhotonView PV;
    public Transform tranformref;

    private void Awake()
    {
        PV = this.GetComponent<PhotonView>();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)

    {
       if (stream.IsWriting&& PV.IsMine)
        {
            Activate = tranformref.gameObject.activeSelf;
            stream.SendNext(Activate);
        }

       else
        {
         

            tranformref.gameObject.SetActive(Activate);
            Activate = (bool)stream.ReceiveNext();
        }

    }
}
