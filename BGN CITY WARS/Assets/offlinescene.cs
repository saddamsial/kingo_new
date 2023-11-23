using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class offlinescene : MonoBehaviour
{
    public bool Offline;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.OfflineMode = Offline;
    }

}
