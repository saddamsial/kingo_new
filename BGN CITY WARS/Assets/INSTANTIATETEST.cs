using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class INSTANTIATETEST : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject Spawn;
    private Transform Spawned;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            Spawned = PhotonNetwork.Instantiate(Spawn.name, transform.position, transform.rotation).transform;
            photonView.RPC("ReParent", RpcTarget.AllBuffered);
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    public void ReParent()
    {
        Spawned.parent = transform.parent;
    }
    public void CallRPCParenting()
    {
        photonView.RPC("ReParent", RpcTarget.AllBuffered); 
    }





}
