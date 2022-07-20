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


        

        
        CustomEvent.Trigger(triggertarget,(" OnPhotonSerializedView"),stream,info);

    
    }
}
