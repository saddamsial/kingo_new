using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Photon.Pun;
using Photon;
using Photon.Realtime;

public class PUNFORRIG : MonoBehaviourPun,IPunObservable
{
    public GameObject target;
    public Transform head;
    public Transform spine;
    public Transform spine2;
    public Transform cam;
    public Transform rig;
    public Transform bodyrig;
    private PhotonView PV;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //CONTROLS ANIMATION RIGGING VIEW IN PHOTON.VIEW
        if (stream.IsWriting)
        {


            stream.SendNext(head.position);

            stream.SendNext(head.rotation);



            stream.SendNext(spine.position);

            stream.SendNext(spine.rotation);



            stream.SendNext(spine2.position);

            stream.SendNext(spine2.rotation);



            stream.SendNext(cam.position);

            stream.SendNext(cam.rotation);



            stream.SendNext(rig.position);

            stream.SendNext(rig.rotation);



            stream.SendNext(bodyrig.position);

            stream.SendNext(bodyrig.rotation);









        }
        else if (stream.IsReading)
        {

            head.position = (Vector3)stream.ReceiveNext();
            head.rotation = (Quaternion)stream.ReceiveNext();


            spine.position = (Vector3)stream.ReceiveNext();
            spine.rotation = (Quaternion)stream.ReceiveNext();



            spine2.position = (Vector3)stream.ReceiveNext();
            spine2.rotation = (Quaternion)stream.ReceiveNext();



            cam.position = (Vector3)stream.ReceiveNext();
            cam.rotation = (Quaternion)stream.ReceiveNext();



            rig.position = (Vector3)stream.ReceiveNext();
            rig.rotation = (Quaternion)stream.ReceiveNext();



            bodyrig.position = (Vector3)stream.ReceiveNext();
            bodyrig.rotation = (Quaternion)stream.ReceiveNext();




        }


    }

    
    public void Start()
      
        
    {
        PV = GetComponent<PhotonView>();
        target = this.gameObject; 
    }

    [PunRPC]
     void  RigWeight()
    {

        CustomEvent.Trigger(target, "RigWeight");

    }

    [PunRPC]
    public void Update()

    {
     if (PV.IsMine)
        {

        PV.RPC("RigWeight", RpcTarget.Others);

        }

           
    }








}
