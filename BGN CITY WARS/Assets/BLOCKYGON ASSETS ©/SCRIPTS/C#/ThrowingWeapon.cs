using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
public class ThrowingWeapon : MonoBehaviour
{
    

 public float ThrowForce;
 public float ThrowSpeed;
 public Vector3 ThrowDirection;
 private Rigidbody rb;
 private PlayerActionsVar RootVarSync;
 private float lastshot = 0f;
 private int Throws = 0;
 public bool Fired;
private PhotonView PV;
public GameObject Throwable;
private Transform Player;
private GameObject GrenadeItem;



    private void OnEnable()
    {
       RootVarSync =  transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<PlayerActionsVar>();
        RootVarSync.Weapontype = 0;
       if (this.transform.childCount <= 1)
       //reset spawned item
       {GrenadeItem = GameObject.Instantiate(Throwable);
       rb = GrenadeItem.GetComponent<Rigidbody>();
       GrenadeItem.transform.parent = this.transform;}
       GrenadeItem.transform.localPosition = new Vector3(0,0,0);
       GrenadeItem.transform.localRotation = new Quaternion(0,0,0,0);
       GrenadeItem.transform.localScale = new Vector3(0.02f,0.02f,0.02f);
  
    }


    void Start()
    {
        Player =  transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;

       
       RootVarSync =  transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<PlayerActionsVar>();
       PV =this.GetComponent<PhotonView>();


    }


    // Update is called once per frame
    void Update()
    {
       if (Time.time > lastshot + 0.2f)
        {    
            Fired = false;   
        }
     
      if(RootVarSync.canfire)
         { //canfire


          if (Input.GetKey(KeyCode.Mouse0)&&PV.IsMine && Time.time > lastshot+ThrowSpeed )
        {
          
            Fired = true;

         

            Throw();             


        }
         }
    }

void Throw()
{
   //track shots fired
     Throws = Throws+1;
     //Reset FireRate
     lastshot = Time.time;
     GrenadeItem.GetComponent<GrenadeScript>().SetOff();
     GrenadeItem.transform.parent=null;
     rb .AddForce(transform.TransformDirection(Player.transform.forward)*ThrowForce, ForceMode.Impulse);
     

  if (this.transform.childCount <= 1)
     {GrenadeItem = GameObject.Instantiate(Throwable);
     GrenadeItem.transform.parent = this.transform;
     GrenadeItem.transform.parent = this.transform;
     GrenadeItem.transform.localPosition = new Vector3(0,0,0);
     GrenadeItem.transform.localRotation = new Quaternion(0,0,0,0);
     GrenadeItem.transform.localScale = new Vector3(0.02f,0.02f,0.02f);
     rb = GrenadeItem.GetComponent<Rigidbody>();}
     
    
     
     }


    










         










}//EC