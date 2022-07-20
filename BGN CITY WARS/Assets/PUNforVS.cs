using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Photon.Pun;
using Photon;

public class PUNforVS : MonoBehaviourPun,IPunObservable
{
    public GameObject triggertarget;
    public Transform head;
    public Transform spine;
    public Transform spine2;
    public Transform cam;
    public Transform rig;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

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




        }














        //  bool writing = stream.IsWriting;
        //  bool reading = stream.IsReading;



        //  CustomEvent.Trigger(triggertarget,(" OnPhotonSerializedView"),writing,reading,info);

    }


        

    
    
}
