using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeWeapon : MonoBehaviour

{
 public int Damage;
 private bool Entered;

 private void OnTriggerEnter(Collider other) 
 {
    if(other != null)

    {
      Debug.Log(other.name+("Entered"));

    if(other.GetComponent<TakeDamage>()!= null)
    {
      
        TakeDamage takedamage = other.GetComponent<TakeDamage>();
        
        takedamage.Takedamage(Damage);
        

        
       
    }
 }

}











}
