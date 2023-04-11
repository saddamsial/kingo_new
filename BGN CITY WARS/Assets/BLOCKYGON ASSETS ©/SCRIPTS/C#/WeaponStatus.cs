using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
 public int CurrentClip;
 public int TotalAmmo;
 public bool MaxedAmmo;
 public bool NoAmmo;

void Update() 
{
    if(MaxedAmmo)
    {
        Invoke("ResetMxedAmmo",0.15f);
    }
}
void ResetMxedAmmo()
{
 MaxedAmmo =false;
}
    
}
