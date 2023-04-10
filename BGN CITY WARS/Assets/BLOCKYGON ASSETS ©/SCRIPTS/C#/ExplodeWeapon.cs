using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeWeapon : MonoBehaviour
{
    public WeaponDATA2 weapontype;
    private WeaponStatus weaponstatus;
    public float radius;
    public int DamageMin;
    public int DamageMid;
    public int DamageMax;
    public float Time;
    public LayerMask layermask;
    private RaycastHit hit;
    private Collider Collided;
    public float throwForce;
   

  
 void OnEnable() 

  {
    
   Explode();
    
  } 



 public void Throw()
 {
    Rigidbody rb = this.GetComponent<Rigidbody>();
    rb.AddForce(transform.forward*throwForce,ForceMode.Impulse);
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
