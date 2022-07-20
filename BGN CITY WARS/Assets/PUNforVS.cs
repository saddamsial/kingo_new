using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Photon.Pun;
using Photon;

public class PUNforVS : MonoBehaviourPun,IPunObservable
{
    public GameObject triggertarget;
    public Transform rig;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            stream.SendNext(rig.position);

            stream.SendNext(rig.rotation);

          }
                else if (stream.IsReading)
        {

            rig.position = (Vector3)stream.ReceiveNext();
            rig.rotation = (Quaternion)stream.ReceiveNext();

        }


                
                
                
                
                
                

   

    


      //  bool writing = stream.IsWriting;
      //  bool reading = stream.IsReading;


        
      //  CustomEvent.Trigger(triggertarget,(" OnPhotonSerializedView"),writing,reading,info);

    }


        

    
    
}
