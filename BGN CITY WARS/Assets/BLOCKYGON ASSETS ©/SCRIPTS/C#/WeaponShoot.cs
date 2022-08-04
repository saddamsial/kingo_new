using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Photon;
using Photon.Pun;
public class WeaponShoot : MonoBehaviour
{
    // Start is called before the first frame update

    public float FireRate = 0.5f;
    private float lastshot = 0f;
    public bool Fire;
    public Collider collided;

    [SerializeField]
    private LayerMask layermask;
    public Vector3  point;


    private Vector3 pos;
    private Vector3 Dir;

    private RaycastHit hit;

    [SerializeField]
    private AudioClip FireSFX;

    [SerializeField]
    private AudioClip BodyShotSFX;

    [SerializeField]
    private AudioClip HeadShotSFX;


    public AudioSource AS;
    
    private Animator animator;

    private Transform PlayerParent;
    
    public float BodyDamage = 0.25f;
    public float HeadDamage = 0.5f;



    //pun variables

    private PhotonView PV;
    private PhotonView TPV;







    private void OnEnable()
    {
        PV = this.GetComponent<PhotonView>();
    }









    void Start()
    {

        PlayerParent = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
        collided = hit.collider;

        AS = GetComponent<AudioSource>();
        animator = PlayerParent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()

    {


        pos = Camera.main.transform.position;
        Dir = Camera.main.transform.forward;



        if (Input.GetKey(KeyCode.Mouse0) == true & PV.IsMine & Time.time > lastshot+ FireRate)
        {

          
            Shoot();

            AS.PlayOneShot(FireSFX, 1f);

        }



    }


    void Shoot()

    {
        //animate
        animator.SetBool("shoot", true);


        //fire
        Physics.Raycast(pos, Dir, out hit, Mathf.Infinity, layermask);
        
            collided = hit.collider;

            point = (hit.point);
            Fire = false;


        //audio

       // AS.PlayOneShot(FireSFX, 1f);





        //Call Methods
        BodyShot();

        HeadShot();

        //Reset FireRate

        lastshot = Time.time;

        //Reset animator

        animator.SetBool("shoot", false);

        




    }





    //check Body hit

    void BodyShot()


    { // SF
       

        if (collided != null & collided.name == "HIT BOX-BODY")

        {

        AS.PlayOneShot(BodyShotSFX, 500f);

            PV.RPC("Bodydamage", RpcTarget.Others);

          //  TPV = collided.GetComponent<PhotonView>();

        }


    } //EF




    void HeadShot()
    { //SF


        if (collided != null & collided.name == "HIT BOX-HEAD")

        {

            AS.PlayOneShot(HeadShotSFX, 400f);

            PV.RPC("Headdamage", RpcTarget.Others);

            //  TPV = collided.GetComponent<PhotonView>();




        }









    }//EF

















    [PunRPC]
    void Bodydamage()
    {//sf

        GameObject player =  GameObject.FindGameObjectWithTag("Player");



      TakeDamage TDF = player.GetComponent<TakeDamage>();

        TDF.Takedamage(BodyDamage);
        Debug.Log("body reached");
        




    }//ef








    [PunRPC]
    void Headdamage()
    {//sf

        GameObject player = GameObject.FindGameObjectWithTag("Player");



        TakeDamage TDF = player.GetComponent<TakeDamage>();

        TDF.Takedamage(HeadDamage);
        Debug.Log("head reached");





    }//ef




















}
