using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;

public class Shotgun : MonoBehaviour
{
    public WeaponDATA WeaponType;
    private int pelletCount;
    private float spreadAngle;
    private float range = 100f;
    public LayerMask layerMask;
    public Transform weaponShoot;
    public Transform cameraTransform;
    private int ammoCount;
    private int maxAmmo = 20;
    private float fireRate = 0.5f;
    private AudioSource audioSource;
    private AudioClip shootSound;
    private AudioClip reloadSound;
     public bool fired;
     public bool pump;
    public bool   NoAmmo;
     public float firedResetSpeed;
    public bool Canfire;
    public bool Reloading = false;
    private float nextFireTime = 0f;
    public Collider collided;
      //pun variables
    private PhotonView PV;
    public PhotonView TPV;
    public bool bodyshotHit;
    public bool headshotHit;
    private Transform PlayerParent;
    bool Aiming;
    private int BulletsFired;
    


private void OnEnable() 
{
  
     pelletCount=WeaponType.Pellets;
    fireRate=WeaponType.FireRate;
    maxAmmo=WeaponType.MaxAmmo;
    ammoCount=WeaponType.CurrentClip;
    pelletCount=WeaponType.Pellets;
    range=WeaponType.WeaponRange;
    pump=WeaponType.Pump;
}

void OnDisable() 
{
    Reloading = false;
    bodyshotHit = false;
    headshotHit = false;

}


 private void Start() 
{
    reloadSound=WeaponType.ReloadSFX;
    shootSound=WeaponType.FireSFX;
    audioSource=this.GetComponent<AudioSource>();
    PlayerParent= transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
    
}
    void Update()
    { 
        //Pair  var with Player
        PlayerParent.GetComponent<PlayerActionsVar>().Fired= fired;
        Aiming=PlayerParent.GetComponentInChildren<PlayerActionsVar>().IsAiming;
        PlayerParent.GetComponent<PlayerActionsVar>().IsReloading=Reloading;
        //change spread for aim
        if (Aiming)
        {
            spreadAngle=WeaponType.AimBulletSpread;
        }
        else
        {
            spreadAngle=WeaponType.DefaultBulletSpread;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && !Reloading)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
          
        }
     
             if (Time.time > nextFireTime -firedResetSpeed)
        {     
           
            fired = false;      
        }
       
          //ammo system

     //NO AMMO SET UP

    if (WeaponType.MaxedAmmo)
    {
        WeaponType.Ammo = WeaponType.MaxAmmo;
    }
        if(WeaponType.CurrentClip < 1 && WeaponType.Ammo == 0)  
        {
            NoAmmo = true;
            BulletsFired = WeaponType.MaxClip;      
        }
        else 
        {
            NoAmmo = false;      
        }
    
         //Ammo Clamp
        if (WeaponType.Ammo <= 0)
        {
            WeaponType.Ammo = 0;
        }

        //Clip Clamp
        if (WeaponType.CurrentClip <= 0)
        {
            WeaponType.CurrentClip = 0;
        }

        if (WeaponType.CurrentClip >= WeaponType.MaxClip)

          {WeaponType.CurrentClip = WeaponType.MaxClip;}
          ///check reload conditions///

        //auto reload

        if (WeaponType.CurrentClip == 0&&!Reloading && ! NoAmmo && WeaponType.Ammo > 0)
        {
          StartCoroutine(Reload());
        }
        //Manual reload
        if (Input.GetKeyDown(KeyCode.R))
    {
        if (WeaponType.CurrentClip < WeaponType.MaxClip && WeaponType.Ammo > 0 && !Reloading)
        {
            StartCoroutine(Reload());
        }
    }

   
    }



void Shoot()
    {
        if (WeaponType.CurrentClip <= 0)
        {
            return;
        }
        //fired var
         fired=true;
         //track shots fired
        BulletsFired = BulletsFired+1;
        audioSource.PlayOneShot(shootSound);
        if(pump)
        {
            StartCoroutine(PumpSFX());
        }

        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 direction = GetSpreadDirection(spreadAngle);
            RaycastHit hit;

            if (Physics.Raycast(weaponShoot.position, direction, out hit, range, layerMask))
            {
                // Do damage to hit object if applicable
                collided=hit.collider;
                TPV = collided.transform.parent.GetComponentInParent<PhotonView>();
                BodyShot();
                HeadShot();

            }

            Debug.DrawRay(weaponShoot.position, direction * range, Color.red, 1f);
        }

        WeaponType.CurrentClip--;
    }

Vector3 GetSpreadDirection(float spread)

    {
        Vector3 direction = cameraTransform.forward;
        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);
        direction.z += Random.Range(-spread, spread);
        return direction.normalized;
    }
//coroutines//
IEnumerator PumpSFX()
{
yield return new WaitForSeconds(0.25f);
audioSource.PlayOneShot(WeaponType.pumpSFX);
}
 IEnumerator Reload()
{
    Reloading = true;
    
    while (WeaponType.CurrentClip < WeaponType.MaxClip && WeaponType.Ammo > 0)
    {
        Debug.Log("reloading");
        audioSource.PlayOneShot(WeaponType.ReloadSFX, 1f);
        
        yield return new WaitForSeconds(WeaponType.ReloadTime);
        
        WeaponType.CurrentClip++;
        WeaponType.Ammo--;
    }
    
    BulletsFired = 0;
    Reloading = false;
}

//ef

//checkbodyshot
void BodyShot()

    { // SF

         if(collided.gameObject.layer==0)
         {
            return;
         }
        if (collided != null && collided.name == "HIT BOX-BODY")


        {

            if (TPV != null)
              //self shoot detect
            if (TPV.IsMine)
            return;
            else // other online player detect
            {
                audioSource.PlayOneShot(WeaponType.BodyshotSFX, 1f);

                PV.RPC("Bodydamage", RpcTarget.Others);

                //  TPV = collided.GetComponent<PhotonView>();

                Debug.Log("Real Player Detected-Body");

                //Hit Reticle Enable
                StartCoroutine(Hitreticle());
            }



            else if (collided.name == "HIT BOX-BODY" & TPV == null)
            {
                      ///AI detct
            if(collided.CompareTag("AI"))
             
            {
            TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();

                audioSource.PlayOneShot(WeaponType.BodyshotSFX, 1f);

                Debug.Log("AI Target Detected-Body");

                //Hit Reticle Enable
                StartCoroutine(Hitreticle());
               takedamage.Takedamage(WeaponType.BodyDamage);

            }

            else
             {

             audioSource.PlayOneShot(WeaponType.BodyshotSFX, 1f);

                Debug.Log("Iron Target Detected-Body");

              TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();
                if (takedamage != null)
               {takedamage.Takedamage(WeaponType.BodyDamage);}

                //Hit Reticle Enable
                StartCoroutine(Hitreticle());


            }
              
            }

        }

        else return;


  IEnumerator Hitreticle()
    {
      bodyshotHit = true;
        yield return new WaitForSeconds(0.25f);
      bodyshotHit = false;
    }



}
//check headshot
void HeadShot()
    { //SF
         if(collided.gameObject.layer==0)
         {
            return;
         }
            if (collided != null && collided.name == "HIT BOX-HEAD")

       {
              Debug.Log (collided);
            if (TPV != null)
              //self shoot detect
            if (TPV.IsMine)
            return;
            else // other online player detect
            {
                audioSource.PlayOneShot(WeaponType.HeadshotSFX, 1f);

                PV.RPC("Headdamage", RpcTarget.Others);

                //  TPV = collided.GetComponent<PhotonView>();

                Debug.Log("Real Player Detected-Head");

                //Hit Reticle Enable
                StartCoroutine(HitHeadreticle());
            }



            else if (collided.name == "HIT BOX-HEAD" && TPV == null)
            {
               
                      ///AI detct
            if(collided.CompareTag("AI"))
             
            {
            TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();

                audioSource.PlayOneShot(WeaponType.HeadshotSFX, 1f);

                Debug.Log("AI Target Detected-Head");

                //Hit Reticle Enable
                StartCoroutine(HitHeadreticle());
               takedamage.Takedamage(WeaponType.HeadDamage);

            }

            else
             {
               

             audioSource.PlayOneShot(WeaponType.HeadshotSFX, 1f);

                Debug.Log("Iron Target Detected-Head");

                //Hit Reticle Enable
                StartCoroutine(HitHeadreticle());

            }            
            }
      }
        

        else return;

         IEnumerator HitHeadreticle()
    {
       headshotHit = true;
        yield return new WaitForSeconds(0.25f);
        headshotHit = false;
    }

    }



}