using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
public class MeleWeapon : MonoBehaviour
{
  //vars
public WeaponDATA WeaponType;
public LayerMask layerMask;
public float Speed;
public float SwingSize;
public float Reach;
public int Swings = 0;
public bool Fired;
private float lastshot = 0f;
public bool Canfire;
public Transform SwingPoint;
public AudioSource AS;
private PhotonView PV;
public PhotonView TPV;
private RaycastHit hit;
public Color color;




    void Start()
    {
        PV =  this.GetComponent<PhotonView>();
    }

 
    void Update()
    {
  
          if (Time.time > lastshot + 0.2f)
        {     
           
            Fired = false;     
   
           
        }



   if(Canfire)
        { //canfire


        if (Input.GetKey(KeyCode.Mouse0) == true & PV.IsMine & Time.time > lastshot+WeaponType.FireRate & WeaponType.CurrentClip >0)
        {
            AS.PlayOneShot(WeaponType.FireSFX, 1f);
            Fired = true;

         

            Swing();             

        }


        }//canfire

    void Swing()
     {

     //track shots fired
     Swings = Swings+1;
     //Reset FireRate
     lastshot = Time.time;
     
     
       //fire
     if (Physics.SphereCast(SwingPoint.position,SwingSize,SwingPoint.forward,out hit,Reach,layerMask))
     {
     Debug.Log(hit.collider.name,hit.collider);

     }
    
  

     }






        void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(SwingPoint.position,SwingSize);
    }
}




































    }










