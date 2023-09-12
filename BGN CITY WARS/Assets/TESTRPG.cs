using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TESTRPG : MonoBehaviour
{
    [SerializeField]
    private bool TestBool;
    PhotonView PV;

    private void Start()
    {
        PV = transform.root.GetComponent<PhotonView>();
    }
    [PunRPC]
    public void TestRPC()
    {
        TestBool = true;
    }
}
