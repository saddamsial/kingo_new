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
public bool ButtonFired;




void OnEnable() 
{
  weapontype=WeaponType.Weapontype;
  
  //PlayerParent.GetComponent<PlayerActionsVar>().Weapontype=weapontype;
}
     void Start()
     {
        Invoke("FindParent", .5f);
       PV =  this.GetComponent<PhotonView>();
       

      }

 void FindParent()
    {
        PlayerParent = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
        SwingPoint = PlayerParent.transform.Find("ShootPos");
    }
    void Update()

     {
        if(transform.parent!= null)
        {
       PlayerParent.GetComponent<PlayerActionsVar>().Fired = Fired;
       Canfire =  PlayerParent.GetComponent<PlayerActionsVar>().canfire;
        }

        if (Time.time > lastshot + 0.2f)
         {     
         Fired = false;     
         }



         if(Canfire)
         { //canfire


          if (ButtonFired && PV.IsMine & Time.time > lastshot+WeaponType.FireRate)
           {
            AS.PlayOneShot(WeaponType.FireSFX, 1f);
            Fired = true;
            Swing();             
       
            }
            else if (ButtonFired==false)
{
  return;
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










