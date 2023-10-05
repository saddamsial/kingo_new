using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Photon;
using Photon.Pun;

public class WeaponShoot : MonoBehaviour
{
    /// variables///
    //enum Datatype {[Tooltip("Pistols,AssaultRifles,ETC")]SingleRay, [Tooltip("Multiple Shots like ShotGun Style ")] MultiRay, [Tooltip("Slow travelong projectile Style like  Rockets ")] Projectile, [Tooltip("CloseRange Direct Contact like a fist or Sword ")] Melee }
    //[SerializeField] Datatype WeaponType;
   // public WeaponDATA WeaponType;
    private WeaponStatus weaponstatus;
    private PlayerActionsVar Parentvariables;
    [Header("Weapon Specs")]
    public float FireRate;
    public float ReloadTime;
    public int BodyDamage;
    public int HeadDamage;
    public int TotalDamageDealt;
    public float WeaponRange;
    private int TargetHP;
    private int TargetShield;

    [Space(10)]
    [Header("Ammo Settings")]
 
    public int currentclip;
    public int MaxClip;
    public int totalammo;
    public int MaxAmmo;
    public int BulletsFired = 0;
    public bool noammo;
    private float lastshot = 0f;
    [Space(10)]
    [Header("Firing Info")]
    public bool Canfire;
    private bool started;
    public bool Fired;
    public bool ButtonFired;
    public float modifiedFireRate;
    [Space(10)]
    [Header("Reload Info")]
    public  bool Reloading;
    public bool ButtonReload;
    public Collider collided;


[HideInInspector]
    public bool bodyshotHit;
[HideInInspector]
    public bool headshotHit;
    [Header("Weapon Settings")]
    [SerializeField]
    private LayerMask layermask;
    private Vector3  point;
    private Transform pos;
    private Transform Shootpoint;
    private RaycastHit hit;
    private RaycastHit hit2;

    [Header("Weapon Audio")]
    [SerializeField]
    private AudioSource AS;
    [SerializeField]
    private AudioClip FireSFX;
    [SerializeField]
    private AudioClip BodyshotSFX;
    [SerializeField]
    private AudioClip HeadshotSFX;
    [SerializeField]
    private AudioClip ReloadSFX;

     private Transform PlayerParent;
    // VFX SPAWN
    [Header("WeaponVFX")]
    public ParticleSystem BulletTrailVFX;
    public ParticleSystem BulletDropVFX;
    public GameObject BulletHoleVFX;

    //pun variables
    [Header("Debugs")]
    private PhotonView PV;
    public GameObject KillFeed;
    [SerializeField]
    private PhotonView TPV;
    private Transform KILLSNOTIFICATION;


    private void OnEnable()
    {
   
    
        Invoke("FindParent", .5f);
        PV = this.GetComponent<PhotonView>();

        collided = hit.collider;

        AS = GetComponent<AudioSource>();
        
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
    void FindParent()
    {
        PlayerParent = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
        weaponstatus = PlayerParent.GetComponent<WeaponStatus>();
    }
    private void Start() 
{
    
    currentclip = MaxClip;
    totalammo=MaxAmmo;
    //WeaponRange = WeaponType.WeaponRange;
    Shootpoint= GameObject.FindGameObjectWithTag("ShootPoint").transform;
        //WeaponRange=WeaponRange;

        KillFeed = GameObject.Find("KILL FEEDS").transform.GetChild(0).gameObject;
        Parentvariables = PlayerParent.GetComponent<PlayerActionsVar>();
        KILLSNOTIFICATION = GameObject.Find("KILLS NOTIFICATION").transform;

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
   modifiedFireRate = 1.0f /FireRate;

    if(TargetShield<1 && TotalDamageDealt >= 100)//ResetTotalDamage
        {
         
            TotalDamageDealt = 0;

         
        }
  
 // CHECK RETICLE HIT(NO SHOOTING)


   //NO AMMO SET UP

    if (weaponstatus.MaxedAmmo)
    {
       totalammo = MaxAmmo;
    }
        if(currentclip < 1 && totalammo == 0)  
        {
            noammo = true;
            BulletsFired =MaxClip;      
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
        BulletsFired =MaxClip - currentclip;
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

        if (currentclip >=MaxClip)

          {currentclip = MaxClip;}

        if (Time.time > lastshot + 0.2f)
        {     
           
            Fired = false;      
        }


        if(Canfire)
        { //canfire


        if (ButtonFired == true && PV.IsMine && Time.time > lastshot + modifiedFireRate && currentclip > 0 && !Reloading && Canfire)
        {
            AS.PlayOneShot(FireSFX, 1f);

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
        if (ButtonReload && !Reloading && !noammo && currentclip < MaxClip && totalammo > 0) 
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

     {   TPV = collided.transform.root.transform.GetChild(0).GetComponentInParent<PhotonView>();
         
        //bullet HOLE SPAWN 
        if(BulletHoleVFX!=null)

            {
                GameObject.Instantiate(BulletHoleVFX, hit.point, transform.localRotation);
            }
       
       
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

                        TargetHP = TPV.GetComponent<TakeDamage>().HP;
                        TargetShield = TPV.GetComponent<TakeDamage>().Shield;

                        AS.PlayOneShot(BodyshotSFX, 1f);

               RpcTarget RPCTYPE = new RpcTarget();
               if (TPV.IsMine && TPV.gameObject.tag == ("CAR"))
               {
                RPCTYPE=RpcTarget.All;
               }
               else RPCTYPE=RpcTarget.Others;

                       Bodydamage();

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

                AS.PlayOneShot(BodyshotSFX, 1f);

                Debug.Log("AI Target Detected-Body");

                //Hit Reticle Enable
                StartCoroutine(Hitreticle());
               takedamage.Takedamage(BodyDamage);

            }

            else
             {

             AS.PlayOneShot(BodyshotSFX, 1f);

                Debug.Log("Iron Target Detected-Body");

              TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();
                if (takedamage != null)
               {takedamage.Takedamage(BodyDamage);}

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
                AS.PlayOneShot(HeadshotSFX, 1f);

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

                AS.PlayOneShot(HeadshotSFX, 1f);

                Debug.Log("AI Target Detected-Head");

                //Hit Reticle Enable
                StartCoroutine(HitHeadreticle());
               takedamage.Takedamage(HeadDamage);

            }

            else
             {
               

             AS.PlayOneShot(HeadshotSFX, 1f);

                Debug.Log("Iron Target Detected-Head");

                //Hit Reticle Enable
                StartCoroutine(HitHeadreticle());


            }
              
            }

        }

        else return;

    }


    }//EF


    void Bodydamage()
    {
        // ...

        // Check if the target is already dead
        if (TargetShield <= 0 && TargetHP < 1)
        {
            // Target is already dead, do not apply damage again
            return;
        }

        TPV.RPC("Takedamage", RpcTarget.All, BodyDamage);
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;

        if (TargetShield <= 0)
        {
            TotalDamageDealt += BodyDamage;
        }

        // Check again after updating TargetHP
        if (TargetShield <= 0 && TargetHP < 1)
        {
            KillFeed.gameObject.SetActive(true);
            Parentvariables.TotalRoomkillsTrack++;

           GameObject Killpopupitem =  PhotonNetwork.Instantiate("KILLS POPUP ITEM", transform.position, Quaternion.identity); // spawn kill UI notification
            Killpopupitem.transform.parent= KILLSNOTIFICATION;
            Killpopupitem.GetComponent<KillPopupManager>().PlayerKilled = TPV.GetComponent<PhotonSerializerBGN>().PlayerNickName;
            Killpopupitem.GetComponent<KillPopupManager>().PlayerKiller = PhotonNetwork.NickName ;
        }
    



    // TDF.Takedamage(WeaponType.BodyDamage);
    Debug.Log("body reached");

        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;



    }//ef

    [PunRPC]
    void Headdamage()
    {//sf

        GameObject player = GameObject.FindGameObjectWithTag("Player");



        TakeDamage TDF = player.GetComponent<TakeDamage>();

        TDF.Takedamage(HeadDamage);
        Debug.Log("head reached");





    }//ef




      /////Coroutines/////

    //Ammo & reload
    IEnumerator Reload()
{
    Reloading = true;
    AS.PlayOneShot(ReloadSFX, 1f);
    noammo = false;

    // Calculate reload time based on the inverse of WeaponType.ReloadSpeed
    float reloadTime = 1.0f / ReloadTime;

    yield return new WaitForSeconds(reloadTime);

    if (totalammo < MaxClip)
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


    }//ef
    //VFX Toggles
    IEnumerator VFX()
    {
        yield return new WaitForSeconds(0.15f);

        BulletTrailVFX.Play();
        BulletDropVFX.Play();
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
