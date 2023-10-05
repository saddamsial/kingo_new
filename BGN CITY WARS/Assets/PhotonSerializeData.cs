using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonSerializeData : MonoBehaviourPunCallbacks,IPunObservable
{
    private KillPopupManager popup;

    private void Start()
    {
        popup = this.GetComponent<KillPopupManager>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
        if (stream.IsWriting)
        {
           stream.SendNext(popup.PlayerKiller);
            stream.SendNext(popup.PlayerKilled);
        }

        else
        {
            popup.PlayerKiller = (string)stream.ReceiveNext();
            popup.PlayerKilled = (string)stream.ReceiveNext();
        }

    }



}
