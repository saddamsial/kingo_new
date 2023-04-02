using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Photon;
using Photon.Pun;

public class ShotgunScript : MonoBehaviour
{
    /// variables///
    public WeaponDATA WeaponType;
    public int BulletsFired = 0;
    private float lastshot = 0f;
    public float WeaponRange;
    public bool Fired;
    public bool Canfire;
    public  bool Reloading;
[HideInInspector]
    public bool bodyshotHit;
[HideInInspector]
    public bool headshotHit;
    public bool NoAmmo;


    public Collider collided;
    [SerializeField]
    private LayerMask layermask;
    private Vector3  point;
    public Vector2 Spread;
    private Transform pos;
    private RaycastHit hit;
    private RaycastHit hit2;
 
    public AudioSource AS;
    private Animator animator;
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
      
        PV = this.GetComponent<PhotonView>();

        collided = hit.collider;

        AS = GetComponent<AudioSource>();
        
         //disable any active vfx uppon weapon switch
         SparkleVFX.SetActive(false);
          // start reload after weapon pull//
        if ( WeaponType.CurrentClip < 1 && NoAmmo != true)
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
    private void Start() {
    WeaponType.CurrentClip = WeaponType.MaxClip;
    WeaponRange = WeaponType.WeaponRange;
    PlayerParent = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
    animator = PlayerParent.GetComponent<Animator>();
   }
    void Update()
    


{//update S
 PlayerParent.GetComponent<PlayerActionsVar>().Fired = Fired;

// CHECK RETICLE HIT(NO SHOOTING)


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
        {
            NoAmmo = false;      
        }
    }

        pos = Camera.main.transform.GetChild(2);
      //Reset BulletsFired Custom conditions(calculate the difference manually)
      if (WeaponType.Ammo == 0 )
      {
        BulletsFired = WeaponType.MaxClip - WeaponType.CurrentClip;
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

        if (Time.time > lastshot + 0.2f)
        {     
           
            Fired = false;      
        }


        if(Canfire)
        { //canfire


        if (Input.GetKey(KeyCode.Mouse0) == true & PV.IsMine & Time.time > lastshot+WeaponType.FireRate & WeaponType.CurrentClip >0 && !Reloading)
        {
            AS.PlayOneShot(WeaponType.FireSFX, 1f);
            Fired = true;

             StartCoroutine(VFX());

            Shoot();             

        }


        }//canfire


        else
        {
            return;
        }

     ///check reload conditions///

        //auto reload

        if (WeaponType.CurrentClip == 0&!Reloading & ! NoAmmo && WeaponType.Ammo > 0)
        {
          StartCoroutine( Reload());
        }
        //Manual reload
        if (Input.GetKey(KeyCode.R) & !Reloading & !NoAmmo & WeaponType.CurrentClip < WeaponType.MaxClip && WeaponType.Ammo > 0) 
        {
            StartCoroutine(Reload());
        } 


    }//update E
    void Shoot()

    {
       
        //track shots fired
        BulletsFired = BulletsFired+1;

        //subtract bullets
        WeaponType.CurrentClip = WeaponType.CurrentClip - 1;

        //Reset FireRate

        lastshot = Time.time;

      //  SparkleVFX.SetActive(false);

        //fire
        Physics.Raycast(pos.position, pos.forward, out hit, WeaponRange, layermask);
        Physics.Raycast(pos.position, pos.forward, out hit, WeaponRange, layermask);


     
        collided = hit.collider;

            point = (hit.point);
       
       

     
     
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


        if (collided != null & collided.name == "HIT BOX-BODY")


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



            else if (collided.name == "HIT BOX-BODY" & TPV == null)
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

            if (collided != null & collided.name == "HIT BOX-HEAD")

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



            else if (collided.name == "HIT BOX-HEAD" & TPV == null)
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
     WeaponType.MaxedAmmo = false; 
    {
        yield return new WaitForSeconds(WeaponType.ReloadTime);

 if     (WeaponType.Ammo < WeaponType.MaxClip)
     {
        
         WeaponType.CurrentClip = WeaponType.CurrentClip + WeaponType.Ammo;
         WeaponType.Ammo = WeaponType.Ammo - BulletsFired;
         BulletsFired = 0;
         Reloading = false;
     }

else
        {
         WeaponType.CurrentClip = WeaponType.CurrentClip + BulletsFired;
         WeaponType.Ammo = WeaponType.Ammo - BulletsFired;
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
