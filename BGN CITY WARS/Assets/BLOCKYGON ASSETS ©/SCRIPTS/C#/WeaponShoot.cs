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
    public int currentclip;
    public int totalammo;
    public int BulletsFired = 0;
    public bool noammo;
    private float lastshot = 0f;
    private float WeaponRange;
    public float modifiedFireRate;
    public bool Fired;
    private bool started;
    public bool Canfire;
    public  bool Reloading;
    public bool ButtonFired;
    public bool ButtonReload;

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
     PlayerParent = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
     weaponstatus=PlayerParent.GetComponent<WeaponStatus>();
     PV = this.GetComponent<PhotonView>();

        collided = hit.collider;

        AS = GetComponent<AudioSource>();
        
         //disable any active vfx uppon weapon switch
         SparkleVFX.SetActive(false);
          // start reload after weapon pull//
        if ( currentclip < 1 && weaponstatus.NoAmmo != true)
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
    
    currentclip = WeaponType.MaxClip;
    totalammo=WeaponType.MaxAmmo;
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
  PlayerParent.GetComponent<WeaponStatus>().CurrentClip=currentclip;
  PlayerParent.GetComponent<WeaponStatus>().TotalAmmo=totalammo;
  PlayerParent.GetComponent<WeaponStatus>().NoAmmo=noammo;
   modifiedFireRate = 1.0f / WeaponType.FireRate;

  
 // CHECK RETICLE HIT(NO SHOOTING)


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
        {
            noammo = false;      
        }
    }

        pos = Camera.main.transform.GetChild(2);
      //Reset BulletsFired Custom conditions(calculate the difference manually)
      if (totalammo== 0 )
      {
        BulletsFired = WeaponType.MaxClip - currentclip;
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

        if (Time.time > lastshot + 0.2f)
        {     
           
            Fired = false;      
        }


        if(Canfire)
        { //canfire


        if (ButtonFired == true && PV.IsMine && Time.time > lastshot + modifiedFireRate && currentclip > 0 && !Reloading && Canfire)
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

        if (currentclip == 0&!Reloading && ! noammo && totalammo > 0)
        {
          StartCoroutine( Reload());
        }
        //Manual reload
        if (ButtonReload && !Reloading && !noammo && currentclip < WeaponType.MaxClip && totalammo > 0) 
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
        BulletsFired +=1;

        //subtract bullets
        currentclip --;

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
            if (TPV.IsMine && TPV.gameObject.tag != ("CAR"))
            return;
            else // other online player detect
            {
                AS.PlayOneShot(WeaponType.BodyshotSFX, 1f);

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



            else if (collided.name == "HIT BOX-BODY" && TPV == null)
            {
                      ///AI detct
            if(collided.CompareTag("AI"))
             
            {
            TakeDamage takedamage = collided.transform.GetComponentInParent<TakeDamage>();

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

        //GameObject player =  GameObject.FindGameObjectWithTag("Player");



      TakeDamage TDF = TPV.GetComponent<TakeDamage>();

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
     noammo = false; 
    {
        yield return new WaitForSeconds(WeaponType.ReloadTime);

 if     (totalammo < WeaponType.MaxClip)
     {
        
         currentclip += totalammo;
         totalammo -= BulletsFired;
         BulletsFired = 0;
         Reloading = false;
     }

else
        {
         currentclip += BulletsFired;
         totalammo -= BulletsFired;
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
