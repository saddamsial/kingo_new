using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SFXmanager : MonoBehaviour
{
    private AudioSource AS;
    private PhotonView PV;

    private void Start()
    {
        AS = this.GetComponent<AudioSource>();
        PV = this.GetComponent<PhotonView>();


        #region SpatialBlend Specify
        if (PV.IsMine)
        {
            AS.spatialBlend = 0;
        }
        else AS.spatialBlend = 1;

        #endregion spatial blend 

    }



}



