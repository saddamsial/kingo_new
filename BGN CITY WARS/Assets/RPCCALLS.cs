using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Photon;
using Photon.Pun;

public class RPCCALLS : MonoBehaviour
{

    public bool IsAiming1;
    public bool IsAIMING2;
    public bool shoot1;
    public bool shoot2;

    private PhotonView PV;
     



    // RPC FOR OTHERS VARIABLES
    private void Start()
    {
        GameObject player = this.gameObject;

        PV = player.GetComponent<PhotonView>();


        
    }

    //CHECK SYNCED VARIABLES AND CALL RPC.

    void Update()
    {

          
        if (IsAiming1 == true & PV.IsMine)

        {

        PV.RPC("ISAIMING", RpcTarget.Others);


        }


        if(IsAiming1 != true & PV.IsMine)
        {

            PV.RPC("ISNOTAIMING", RpcTarget.Others);
        }

    }


    //RPC FUNCTIOMS TO CALL IN OTHER PLAYERS.

    [PunRPC]
    void ISAIMING()
    {
     
            IsAIMING2 = true;    
    }

    [PunRPC]
    void ISNOTAIMING()
    {
        IsAIMING2 = false;
    }

































}
