using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Photon;
using Photon.Pun;

public class WeaponShoot : MonoBehaviour
{
    /// variables///
    public WeaponDATA WeaponType;
    private WeaponStatus weaponstatus;
    public int BulletsFired = 0;
    private float lastshot = 0f;
    private float WeaponRange;
    public bool Fired;
    private bool started;
    public bool Canfire;
    public  bool Reloading;
[HideInInspector]
    public bool bodyshotHit;
[HideInInspector]
    public bool headshotHit;
    


    public Collider collided;
    [SerializeField]
    private LayerMask layermask;
    private Vector3  point;
    private Transform pos;
    private Transform Shootpoint;
    private RaycastHit hit;
    private RaycastHit hit2;
 
    public AudioSource AS;
   
     private Transform PlayerParent;
    // VFX SPAWN
    public ParticleSystem BulletTrailVFX;
    public ParticleSystem BulletDropVFX;
    public GameObject SparkleVFX;

  
    //pun variables
    private PhotonView PV;
    public PhotonView TPV;


    private void OnEnable()
    {
     PlayerParent = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
     weaponstatus=PlayerParent.GetComponent<WeaponStatus>();
        PV = this.GetComponent<PhotonView>();

        collided = hit.collider;

        AS = GetComponent<AudioSource>();
        
         //disable any active vfx uppon weapon switch
         SparkleVFX.SetActive(false);
          // start reload after weapon pull//
        if ( weaponstatus.CurrentClip < 1 && weaponstatus.NoAmmo != true)
        {
            StartCoroutine(Reload());
        }

    }
    private void OnDisable() 
    {
    Reloading = false;
    bodyshotHit = false;
    headshotHit = false;

    }
    private void Start() 
{
    
    weaponstatus.CurrentClip = WeaponType.MaxClip;
    WeaponRange = WeaponType.WeaponRange;
    Shootpoint= GameObject.FindGameObjectWithTag("ShootPoint").transform;
    WeaponRange=WeaponType.WeaponRange;
    
}
void Update()
 {//update S
 //var sync with plaayer
  PlayerParent.GetComponent<PlayerActionsVar>().Fired = Fired;
  PlayerParent.GetComponent<PlayerActionsVar>().IsReloading=Reloading;
  Canfire= PlayerParent.GetComponent<PlayerActionsVar>().canfire;
  
  
 // CHECK RETICLE HIT(NO SHOOTING)


   //NO AMMO SET UP

    if (weaponstatus.MaxedAmmo)
    {
        weaponstatus.TotalAmmo = WeaponType.MaxAmmo;
    }
        if(weaponstatus.CurrentClip < 1 && weaponstatus.TotalAmmo == 0)  
        {
            weaponstatus.NoAmmo = true;
            BulletsFired = WeaponType.MaxClip;      
        }
        
        else 
    {
        {
            weaponstatus.NoAmmo = false;      
        }
    }

        pos = Camera.main.transform.GetChild(2);
      //Reset BulletsFired Custom conditions(calculate the difference manually)
      if (weaponstatus.TotalAmmo == 0 )
      {
        BulletsFired = WeaponType.MaxClip - weaponstatus.CurrentClip;
      }


        //Ammo Clamp
        if (weaponstatus.TotalAmmo <= 0)
        {
            weaponstatus.TotalAmmo = 0;
        }

        //Clip Clamp
        if (weaponstatus.CurrentClip <= 0)
        {
            weaponstatus.CurrentClip = 0;
        }

        if (weaponstatus.CurrentClip >= WeaponType.MaxClip)

          {weaponstatus.CurrentClip = WeaponType.MaxClip;}

        if (Time.time > lastshot + 0.2f)
        {     
           
            Fired = false;      
        }


        if(Canfire)
        { //canfire


        if (Input.GetKey(KeyCode.Mouse0) == true && PV.IsMine && Time.time > lastshot+WeaponType.FireRate && weaponstatus.CurrentClip >0 && !Reloading&&Canfire)
        {
            AS.PlayOneShot(WeaponType.FireSFX, 1f);

             StartCoroutine(VFX());

           float nextActionTime = 0.0f;
           float period = 5f;
             if (Time.time > nextActionTime)
               {
                nextActionTime += period;
                Shoot();
               }
           
                      

        }


        }//canfire


        else
        {
            return;
        }

     ///check reload conditions///

        //auto reload

        if (weaponstatus.CurrentClip == 0&!Reloading && ! weaponstatus.NoAmmo && weaponstatus.TotalAmmo > 0)
        {
          StartCoroutine( Reload());
        }
        //Manual reload
        if (Input.GetKey(KeyCode.R) && !Reloading && !weaponstatus.NoAmmo && weaponstatus.CurrentClip < WeaponType.MaxClip && weaponstatus.TotalAmmo > 0) 
        {
            StartCoroutine(Reload());
            
        } 


    }//update E



  void Shoot()

    {
        
        started=true;
       Fired = true;
       //yield return new WaitForSeconds(0f);
        //track shots fired
        BulletsFired = BulletsFired+1;

        //subtract bullets
        weaponstatus.CurrentClip = weaponstatus.CurrentClip - 1;

        //Reset FireRate

        lastshot = Time.time;

      //  SparkleVFX.SetActive(false);

        //fire
        Physics.Raycast(Shootpoint.position, pos.forward, out hit, WeaponRange, layermask);


     
        collided = hit.collider;

            point = (hit.point);
             started=false;
       
       

     
     
   if (collided == null)
    {return;}
  else

     {   TPV = collided.transform.parent.GetComponentInParent<PhotonView>();
         
        //bullet HOLE SPAWN 
        GameObject.Instantiate(WeaponType.BulletHoleVFX,hit.point,transform.localRotation);
       
        //Call Methods
 
        BodyShot();

        HeadShot();
       

    }

    //check Bodyshot

    void BodyShot()


    { // SF


        if (collided != null && collided.name == "HIT BOX-BODY")


        {

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

                //Hit Reticle Enable
                StartCoroutine(Hitreticle());
            }



            else if (collided.name == "HIT BOX-BODY" && TPV == null)
            {
                      ///AI detct
            if(collided.CompareTag("AI"))
             
            {
            TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();

                AS.PlayOneShot(WeaponType.BodyshotSFX, 1f);

                Debug.Log("AI Target Detected-Body");

                //Hit Reticle Enable
                StartCoroutine(Hitreticle());
               takedamage.Takedamage(WeaponType.BodyDamage);

            }

            else
             {

             AS.PlayOneShot(WeaponType.BodyshotSFX, 1f);

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

    } //EF
   //check headshot
     void HeadShot()
    { //SF

            if (collided != null && collided.name == "HIT BOX-HEAD")

       {
              Debug.Log (collided);
            if (TPV != null)
              //self shoot detect
            if (TPV.IsMine)
            return;
            else // other online player detect
            {
                AS.PlayOneShot(WeaponType.HeadshotSFX, 1f);

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

                AS.PlayOneShot(WeaponType.HeadshotSFX, 1f);

                Debug.Log("AI Target Detected-Head");

                //Hit Reticle Enable
                StartCoroutine(HitHeadreticle());
               takedamage.Takedamage(WeaponType.HeadDamage);

            }

            else
             {
               

             AS.PlayOneShot(WeaponType.HeadshotSFX, 1f);

                Debug.Log("Iron Target Detected-Head");

                //Hit Reticle Enable
                StartCoroutine(HitHeadreticle());


            }
              
            }

        }

        else return;

    }


    }//EF

    [PunRPC]
    void Bodydamage()
    {//sf

        GameObject player =  GameObject.FindGameObjectWithTag("Player");



      TakeDamage TDF = player.GetComponent<TakeDamage>();

        TDF.Takedamage(WeaponType.BodyDamage);
        Debug.Log("body reached");
        




    }//ef

    [PunRPC]
    void Headdamage()
    {//sf

        GameObject player = GameObject.FindGameObjectWithTag("Player");



        TakeDamage TDF = player.GetComponent<TakeDamage>();

        TDF.Takedamage(WeaponType.HeadDamage);
        Debug.Log("head reached");





    }//ef




      /////Coroutines/////

    //Ammo & reload
    IEnumerator Reload()

    {//sf
   
     Reloading = true;
     AS.PlayOneShot(WeaponType.ReloadSFX, 1f);
     weaponstatus.MaxedAmmo = false; 
    {
        yield return new WaitForSeconds(WeaponType.ReloadTime);

 if     (weaponstatus.TotalAmmo < WeaponType.MaxClip)
     {
        
         weaponstatus.CurrentClip = weaponstatus.CurrentClip + weaponstatus.TotalAmmo;
         weaponstatus.TotalAmmo = weaponstatus.TotalAmmo - BulletsFired;
         BulletsFired = 0;
         Reloading = false;
     }

else
        {
         weaponstatus.CurrentClip = weaponstatus.CurrentClip + BulletsFired;
         weaponstatus.TotalAmmo = weaponstatus.TotalAmmo - BulletsFired;
         BulletsFired = 0;
         Reloading = false;
        }
    
       
        }


    }//ef
    //VFX Toggles
    IEnumerator VFX()
    {
        yield return new WaitForSeconds(0.15f);

        BulletTrailVFX.Play();
        SparkleVFX.SetActive(true);
        BulletDropVFX.Play();
        yield return new WaitForSeconds(0.15f);
        SparkleVFX.SetActive(false);

    }
    // Hit Reticles Toggle
    IEnumerator Hitreticle()
    {
      bodyshotHit = true;
        yield return new WaitForSeconds(0.25f);
      bodyshotHit = false;
    }
    IEnumerator HitHeadreticle()
    {
       headshotHit = true;
        yield return new WaitForSeconds(0.25f);
         headshotHit = false;
    }


}//EC
