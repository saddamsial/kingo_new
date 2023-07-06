using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;

public class Shotgun : MonoBehaviour
{
    public WeaponDATA WeaponType;
    private WeaponStatus weaponstatus;
    public float FireDelay;
    private int pelletCount;
    private float spreadAngle;
    private float range = 100f;
    public LayerMask layerMask;
    public Transform weaponShoot;
    public Transform cameraTransform;
    public int currentclip;
    public int totalammo;
    public bool noammo;
    
    public bool MaxedAmmo;
    private float fireRate = 0.5f;
    private AudioSource audioSource;
    private AudioClip shootSound;
    private AudioClip reloadSound;
     public bool fired;
     public bool pump;

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
    public GameObject TrailVFX;
    public GameObject SparkleVFX;
    public ParticleSystem BulletDropVFX;
private void OnEnable() 
{
    PlayerParent= transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
    weaponstatus=PlayerParent.GetComponent<WeaponStatus>();
    pelletCount=WeaponType.Pellets;
    fireRate=WeaponType.FireRate;
    pelletCount=WeaponType.Pellets;
    range=WeaponType.WeaponRange;
    pump=WeaponType.Pump;
    PV = this.GetComponent<PhotonView>();
    // start reload after weapon pull//
    if ( currentclip < 1 && weaponstatus.NoAmmo != true)
    {
    StartCoroutine(Reload());
    }
    //disable any active vfx uppon weapon switch
    SparkleVFX.SetActive(false);
}

void OnDisable() 
{
    Reloading = false;
    bodyshotHit = false;
    headshotHit = false;
}
 private void Start() 
{   currentclip = WeaponType.MaxClip;
    totalammo=WeaponType.MaxAmmo;
    reloadSound=WeaponType.ReloadSFX;
    shootSound=WeaponType.FireSFX;
    audioSource=this.GetComponent<AudioSource>(); 
}
void Update()
{ 
        //Pair  var with Player
        PlayerParent.GetComponent<PlayerActionsVar>().Fired= fired;
        Aiming=PlayerParent.GetComponentInChildren<PlayerActionsVar>().IsAiming;
        PlayerParent.GetComponent<PlayerActionsVar>().IsReloading=Reloading;
        PlayerParent.GetComponent<WeaponStatus>().NoAmmo=noammo;
        PlayerParent.GetComponent<WeaponStatus>().CurrentClip=currentclip;
        PlayerParent.GetComponent<WeaponStatus>().TotalAmmo=totalammo;
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

    if (weaponstatus.MaxedAmmo)
    {
       totalammo = WeaponType.MaxAmmo;
    }
        if(currentclip < 1 && totalammo == 0)  
        {
            noammo = true;
            BulletsFired = WeaponType.MaxClip;      
        }
        else 
        {
            weaponstatus.NoAmmo = false;      
        }
    
       //Ammo Clamp
        if (totalammo <= 0)
        {
            totalammo = 0;
        }

        //Clip Clamp
        if (currentclip <= 0)
        {
            currentclip = 0;
        }

        if (currentclip >= WeaponType.MaxClip)

          {currentclip = WeaponType.MaxClip;}
          ///check reload conditions///

        //auto reload

        if (currentclip == 0&&!Reloading && ! noammo && totalammo > 0)
        {
          StartCoroutine(Reload());
        }
        //Manual reload
        if (Input.GetKeyDown(KeyCode.R))
    {
        if (currentclip < WeaponType.MaxClip && totalammo > 0 && !Reloading)
        {
            StartCoroutine(Reload());
        }
    }

   
}



void Shoot()
    {
        if (currentclip <= 0)
        {
            return;
        }
        //fired var
         fired=true;
         //track shots fired
        BulletsFired = BulletsFired+1;
        audioSource.PlayOneShot(shootSound);
        //vfx
        StartCoroutine(VFX());
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

        currentclip--;
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
    
    while (!Input.GetButton("Fire1")&& currentclip < WeaponType.MaxClip && totalammo > 0)
    {
        Debug.Log("reloading");
        audioSource.PlayOneShot(WeaponType.ReloadSFX, 1f);
        
        yield return new WaitForSeconds(WeaponType.ReloadTime);
        
        currentclip++;
        totalammo--;
        weaponstatus.MaxedAmmo=false;
    }
    
    BulletsFired = 0;
    Reloading = false;
}

//ef

//checkbodyshot
void BodyShot()

    { // SF
     
        if (collided != null && collided.name == "HIT BOX-BODY")


        {

            if (TPV != null)
              //self shoot detect
            if (TPV.IsMine && TPV.gameObject.tag != ("CAR"))
            return;
            else // other online player detect
            {
                audioSource.PlayOneShot(WeaponType.BodyshotSFX, 1f);

               RpcTarget RPCTYPE = new RpcTarget();
               if (TPV.IsMine && TPV.gameObject.tag == ("CAR"))
               {
                RPCTYPE=RpcTarget.All;
               }
               else RPCTYPE=RpcTarget.Others;

                PV.RPC("Bodydamage", RPCTYPE);

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
            TakeDamage takedamage = TPV.GetComponent<TakeDamage>();

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

//VFX

    IEnumerator VFX()
    {
        yield return new WaitForSeconds(0.15f);
        TrailVFX.SetActive(true);
        SparkleVFX.SetActive(true);
        BulletDropVFX.Play();
        yield return new WaitForSeconds(0.15f);
        SparkleVFX.SetActive(false);

    }


}