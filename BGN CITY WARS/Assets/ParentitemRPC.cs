using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class ParentitemRPC : MonoBehaviour
{
    public Transform ParentTransform;
    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = this.GetComponent<PhotonView>();
      
    }

 
    [PunRPC]
    public void ParentItem()
    {

        if (transform.parent == null)
        transform.parent = ParentTransform;

        Debug.Log("PunRPc reached for Skin");
    }
    private void Update()
    {
        PV.RPC("ParentItem", RpcTarget.AllBuffered);

    }
}
