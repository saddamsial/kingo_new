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
private int Swings = 0;
public bool Fired;
private float lastshot = 0f;
public bool Canfire;
public Transform SwingPoint;
public AudioSource AS;
private PhotonView PV;
public PhotonView TPV;
private RaycastHit hit;
private Collider collided;
private Transform PlayerParent;
public int weapontype;




void OnEnable() 
{
  weapontype=WeaponType.Weapontype;
  PlayerParent=transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
  //PlayerParent.GetComponent<PlayerActionsVar>().Weapontype=weapontype;
}
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


          if (Input.GetKey(KeyCode.Mouse0) && PV.IsMine & Time.time > lastshot+WeaponType.FireRate & WeaponType.CurrentClip >0)
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
     if (Physics.SphereCast(SwingPoint.position,SwingSize,SwingPoint.forward,out hit,1,layerMask))
     {
      collided = hit.collider;
     Debug.Log(hit.collider.name,hit.collider);

     
     BodyShot();

     }

   
}

 void BodyShot()


    { // SF


        if (collided != null & collided.name == "HIT BOX-BODY")


        {
          Debug.Log("reached Bodyshot();");
          
            if (TPV != null)
            
            
              //self shoot detect
            if (TPV.IsMine)
            return;
            else // other online player detect
            {
                AS.PlayOneShot(WeaponType.BodyshotSFX, 1f);

                PV.RPC("Bodydamage", RpcTarget.Others);

                //  TPV = collided.GetComponent<PhotonView>();

                Debug.Log("Real Player Detected-Body");

            }

            else if (collided.name == "HIT BOX-BODY" & TPV == null)
            {
               Debug.Log("reached before tag();");
                      ///AI detct
            if(collided.CompareTag("AI"))
             
            {
            TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();

                AS.PlayOneShot(WeaponType.BodyshotSFX, 1f);

                Debug.Log("AI Target Detected-Body");

             
               takedamage.Takedamage(WeaponType.BodyDamage);

            }

            else
             {

             AS.PlayOneShot(WeaponType.BodyshotSFX, 1f);

                Debug.Log("Iron Target Detected-Body");

            }
              
            }

        }

        else return;

    } //EF







    }











    }//END CLASS










