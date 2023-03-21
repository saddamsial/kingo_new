using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeWeapon : MonoBehaviour
{
    public float radius;
    public float DamageMin;
    public float DamageMid;
    public float DamageMax;
    public float Time;
    public LayerMask layermask;
    private RaycastHit hit;
    private Collider Collided;
   

  
 void OnEnable() 

  {
    
   Explode();
    
  } 



    void Explode()
{
    Debug.Log("Triggered");
    if (Physics.SphereCast(transform.position,radius,new Vector3 (0,0,0), out hit,1,layermask) )
    {
    
    Debug.Log(Collided);
     Collided = hit.collider;
      Debug.Log(Collided);
        if (Collided!=null && Collided.GetComponent<TakeDamage>()!= null)
        {
         TakeDamage takeDamage = hit.collider.GetComponent<TakeDamage>();

         takeDamage.Takedamage(DamageMid);
         
 
        }
       

    }
}















}//EC
