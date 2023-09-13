using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class ParentitemRPC : MonoBehaviour
{
    private PhotonView PV;
    public GameObject instantiatedItem;
    // Start is called before the first frame update
    void Start()
    {
        PV = this.GetComponent<PhotonView>();
      
    }

 
    [PunRPC]
    public void ParentItem()
    {


        instantiatedItem.transform.parent = gameObject.transform ;

        Debug.Log("PunRPc reached for Skin");

    }
    private void Update()
    {
        PV.RPC("ParentItem", RpcTarget.All);

    }
}
