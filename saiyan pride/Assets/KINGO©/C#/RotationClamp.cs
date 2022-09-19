using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationClamp : MonoBehaviour
{


     public float minAngle = -20;
     public float maxAngle = 40;    
 
     private void Update()
     {
         // get relative range +/-
         float relRange = (maxAngle - minAngle) / 2f;
 
         // calculate offset
         float offset = maxAngle - relRange;
 
         // convert to a relative value
         Vector3 angles = turretToClamp.turret.transform.eulerAngles;
         float z = ((angles.z + 540) % 360) - 180 - offset;
 
         // if outside range
         if (Mathf.Abs(z) > relRange)
         {
             angles.z = relRange * Mathf.Sign(z) + offset;
             turretToClamp.turret.transform.eulerAngles = angles;
         }
     }

















}
