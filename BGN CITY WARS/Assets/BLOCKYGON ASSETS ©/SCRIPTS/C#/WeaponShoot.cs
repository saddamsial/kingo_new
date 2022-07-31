using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class WeaponShoot : MonoBehaviour
{
    // Start is called before the first frame update


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

    public AudioSource AS;
    
    private Animator animator;

    private Transform PlayerParent;
    public float BodyDamage = 0.25f;
    public float HeadDamage = 0.5f;

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



        if (Fire== true )
        {

          
            Shoot();
            BodyShot();
           

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

        AS.PlayOneShot(FireSFX,1f);




        



    }





    //check Body hit

    void BodyShot()


    { // SF
      
        if (collided != null & collided.name == "HIT BOX-BODY")

        {

        AS.PlayOneShot(BodyShotSFX, 500f);
           
            Bodydamage();


        }


    } //EF


    void Bodydamage()
    {//sf
        


       TakeDamage TDF = collided.GetComponent<TakeDamage>();

        TDF.Takedamage(BodyDamage);
        




    }//ef
















}
