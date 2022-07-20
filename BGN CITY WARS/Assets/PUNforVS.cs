using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Photon.Pun;
using Photon;
public class PUNforVS : MonoBehaviour
{
    public GameObject triggertarget;
    
   private void OnPhotonSerializedView(PhotonStream stream,PhotonMessageInfo info)

    {


        bool writing = stream.IsWriting;
        bool reading = stream.IsReading;


        
        CustomEvent.Trigger(triggertarget,(" OnPhotonSerializedView"),writing,reading,info);

    
    }
}
