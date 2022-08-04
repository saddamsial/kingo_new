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
   // public float FireRate = 0.5f;
    private float lastshot = 0f;
    public bool Fired;
    public int BulletsFired = 0;


    private float BeginReloadTime;

    [SerializeField]
    private bool Reloading;
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
    public GameObject SparkleVFX;

   

    //pun variables

    private PhotonView PV;
    private PhotonView TPV;



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

        if (Time.time > lastshot + 0.1f)
        {

             SparkleVFX.SetActive(false);

            //Reset animator

             animator.SetBool("shoot", false);
            Fired = false;


        }





        if (Input.GetKey(KeyCode.Mouse0) == true & PV.IsMine & Time.time > lastshot+WeaponType.FireRate& Clip >0)
        {


            BulletTrailVFX.Play();

            SparkleVFX.SetActive(true);

            AS.PlayOneShot(WeaponType.FireSFX, 1f);
            Fired = true;

            Shoot();





          

        }









        //check reload conditions

        if (Clip == 0&!Reloading & ! NoAmmo)
        {
          StartCoroutine( Reload());
        }

        if (Input.GetKey(KeyCode.R)&!Reloading & !NoAmmo)
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

        //animate
        animator.SetBool("shoot", true);

        //Reset FireRate

        lastshot = Time.time;


      //  SparkleVFX.SetActive(false);



        //fire
        Physics.Raycast(pos, Dir, out hit, Mathf.Infinity, layermask);


        collided = hit.collider;

            point = (hit.point);
         
        //bullet HOLE SPAWN 
        GameObject.Instantiate(WeaponType.BulletHoleVFX,hit.point,Quaternion.Euler(90,0,0));
       

        //Call Methods
        BodyShot();

        HeadShot();



    }




    //check Body hit

    void BodyShot()


    { // SF
       

        if (collided != null & collided.name == "HIT BOX-BODY")

        {

        AS.PlayOneShot(WeaponType.BodyshotSFX, 500f);

            PV.RPC("Bodydamage", RpcTarget.Others);

          //  TPV = collided.GetComponent<PhotonView>();

        }


    } //EF




    void HeadShot()
    { //SF


        if (collided != null & collided.name == "HIT BOX-HEAD")

        {

            AS.PlayOneShot(WeaponType.HeadshotSFX, 400f);

            PV.RPC("Headdamage", RpcTarget.Others);

            //  TPV = collided.GetComponent<PhotonView>();




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












    //Ammo & reload



    
    IEnumerator Reload()

    {//sf
     
  
        Reloading = true;


        {

            yield return new WaitForSeconds(WeaponType.ReloadTime);

        Clip = Clip + BulletsFired;

        Ammo = Ammo - BulletsFired;

        BulletsFired = 0;

         Reloading = false;

        }








    }//ef




















}//EC
