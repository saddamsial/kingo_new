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
    public GameObject Target;
    public PhotonView PVTarget;










    private void OnEnable()
    {
        PV = this.GetComponent<PhotonView>();
    }




    void Start()

    {

        PV = this.GetComponent<PhotonView>();

      //  PVTarget = Target.GetComponent<PhotonView>();

    }

   









    void Update()
    {

        PVTarget = Target.GetComponent<PhotonView>();

        if (BODYSHOT == true & PV.IsMine)

        {


            PV.RPC("BodyShot",RpcTarget.Others);




        }









        if ( HEADSHOT == true & PV.IsMine)

        {


            PV.RPC("HeadShot", RpcTarget.Others);



        }




   

    }






    [PunRPC]
    void BodyShot()
    {


        CustomEvent.Trigger(this.gameObject, "TAKEDAMAGE", bodydamage);

        Debug.Log("test");

    }




    [PunRPC]
    void HeadShot()
    {


        CustomEvent.Trigger(PVTarget.gameObject, "TAKEDAMAGE", headshotdamage);

    }











}
