using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Photon;
using Photon.Pun;

public class WeaponShoot : MonoBehaviour
{
    // Start is called before the first frame update
    public WeaponDATA WeaponType;
  
    private float lastshot = 0f;
    public bool Fired;
    public int BulletsFired = 0;
    public bool Canfire;
 
    public  bool Reloading;
    [SerializeField]
    private bool NoAmmo;
    public int Clip;
    public int Ammo;


    public Collider collided;

    [SerializeField]
    private LayerMask layermask;
    public Vector3  point;


    private Vector3 pos;
    private Vector3 Dir;

    private RaycastHit hit;


    public AudioSource AS;
   
    private Animator animator;

    private Transform PlayerParent;
   
    // VFX SPAWN
    public ParticleSystem BulletTrailVFX;
    public ParticleSystem BulletDropVFX;
    public GameObject SparkleVFX;

    //HitReticleS
    public GameObject HitReticle;
    public GameObject HitHeadReticle;

    //UI TEXT
    public GameObject ReloadingTextUI;
    public GameObject NoAmmoTextUI;


    //pun variables
    private PhotonView PV;
    public PhotonView TPV;



    private void OnEnable()
    {
        PV = this.GetComponent<PhotonView>();


        PlayerParent = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
        collided = hit.collider;

        AS = GetComponent<AudioSource>();
        animator = PlayerParent.GetComponent<Animator>();

    }



    // Update is called once per frame
    void Update()

    {
       
        if (Ammo == 0)
        {
            NoAmmo = true;
        }



        pos = Camera.main.transform.position;
        Dir = Camera.main.transform.forward;



        //Ammo Clamp
        if (Ammo <= 0)
        {
            Ammo = 0;
        }

        //Clip Clamp
        if (Clip <= 0)
        {
            Clip = 0;
        }



        //ammo sync
        WeaponType.Clip = Clip;
        WeaponType.Ammo = Ammo;
        //reloading text
        ReloadingTextUI.SetActive(Reloading);
        //NoAmmo text
        if(Clip < 1) { NoAmmoTextUI.SetActive(NoAmmo); }
        

        if (Time.time > lastshot + 0.2f)
        {     
           
            Fired = false;      
        }


        if(Canfire)
        { //canfire


        if (Input.GetKey(KeyCode.Mouse0) == true & PV.IsMine & Time.time > lastshot+WeaponType.FireRate & Clip >0)
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

        if (Clip == 0&!Reloading & ! NoAmmo)
        {
          StartCoroutine( Reload());
        }
        //Manual reload
        if (Input.GetKey(KeyCode.R) & !Reloading & !NoAmmo & Clip < WeaponType.MaxClip)
        {
            StartCoroutine(Reload());
        }


    }


    void Shoot()

    {
       

        //track shots fired
        BulletsFired = BulletsFired+1;

        //subtract bullets
        Clip = Clip - 1;

        
    

        //Reset FireRate

        lastshot = Time.time;


      //  SparkleVFX.SetActive(false);



        //fire
        Physics.Raycast(pos, Dir, out hit, Mathf.Infinity, layermask);


        collided = hit.collider;

            point = (hit.point);

        TPV = collided.GetComponentInParent<PhotonView>();
         
        //bullet HOLE SPAWN 
        GameObject.Instantiate(WeaponType.BulletHoleVFX,hit.point,transform.localRotation);
       

        //Call Methods
        BodyShot();

        HeadShot();



    }




    //check Body hit

    void BodyShot()


    { // SF


        if (collided != null & collided.name == "HIT BOX-BODY")


        {

            if (TPV != null)


            {
                AS.PlayOneShot(WeaponType.BodyshotSFX, 500f);

                PV.RPC("Bodydamage", RpcTarget.Others);

                //  TPV = collided.GetComponent<PhotonView>();

                Debug.Log("Real Player Detected-Body");

                //Hit Reticle Enable
                StartCoroutine(Hitreticle());
            }



            else if (collided.name == "HIT BOX-BODY" & TPV == null)
            {


                AS.PlayOneShot(WeaponType.BodyshotSFX, 500f);

                Debug.Log("Iron Target Detected-Body");

                //Hit Reticle Enable
                StartCoroutine(Hitreticle());


            }














        }


     

        else return;




    } //EF


    void HeadShot()
    { //SF


        if (collided != null & collided.name == "HIT BOX-HEAD" & TPV != null)

        {
            if (!TPV.IsMine)
            { 

            AS.PlayOneShot(WeaponType.HeadshotSFX, 400f);

            PV.RPC("Headdamage", RpcTarget.Others);

                //  TPV = collided.GetComponent<PhotonView>();

            Debug.Log("Real Player Detected-Head");


             //Hit Reticle Enable
             StartCoroutine(HitHeadreticle());

            }
        }


        else if (collided.name == "HIT BOX-HEAD" & TPV == null)
        {

            AS.PlayOneShot(WeaponType.HeadshotSFX, 400f);
    
            Debug.Log("Iron Target Detected-Head");

            //Hit Reticle Enable
            StartCoroutine(HitHeadreticle());

        }

        else return;




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
      AS.PlayOneShot(WeaponType.ReloadSFX, 1);
        
        
        {

            yield return new WaitForSeconds(WeaponType.ReloadTime);

        Clip = Clip + BulletsFired;

        Ammo = Ammo - BulletsFired;

        BulletsFired = 0;

         Reloading = false;

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
        HitReticle.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        HitReticle.SetActive(false);
    }

    IEnumerator HitHeadreticle()
    {
        HitHeadReticle.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        HitHeadReticle.SetActive(false);
    }




   



}//EC
