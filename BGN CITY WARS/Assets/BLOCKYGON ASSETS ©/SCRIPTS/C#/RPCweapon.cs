using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Unity.VisualScripting;




public class RPCweapon : MonoBehaviour
{

    public bool BODYSHOT;
    public bool HEADSHOT;
    public float headshotdamage;
    public float bodydamage;
    public PhotonView PV;
    public Transform Target;
    public PhotonView PVTarget;

    void Start()

    {

        PV = this.GetComponent<PhotonView>();

        PVTarget = Target.GetComponent<PhotonView>();

    }

   









    void Update()
    {



        if (BODYSHOT ==true & PV.IsMine)

        {


            PV.RPC("BodyShot", RpcTarget.All);



        }









        if ( HEADSHOT == true & PV.IsMine)

        {


            PV.RPC("HeadShot", RpcTarget.Others);



        }














    }






    [PunRPC]
    void BodyShot()
    {


        CustomEvent.Trigger(PVTarget.gameObject, "TAKEDAMAGE", bodydamage);

        Debug.Log("test");

    }




    [PunRPC]
    void HeadShot()
    {


        CustomEvent.Trigger(PVTarget.gameObject, "TAKEDAMAGE", headshotdamage);

    }











}
