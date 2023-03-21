using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
public class ThrowingWeapon : MonoBehaviour
{
    
 public Vector3 Direction;
 public float ThrowForce;
 public float ThrowSpeed;
 private Rigidbody rb;
 private PlayerActionsVar RootVarSync;
 private float lastshot = 0f;
 private int Throws = 0;
 public bool Fired;
private PhotonView PV;
public GameObject Throwable;
private Transform Player;

    private void OnEnable()
    {
        RootVarSync.Weapontype = 0;

    }



    void Start()
    {
        Player =  transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;

      rb =  Throwable .GetComponent<Rigidbody>();
       RootVarSync =  transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<PlayerActionsVar>();
     PV = Player.GetComponent<PhotonView>();


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
    
     GameObject.Instantiate(Throwable,transform.position,Quaternion.identity);
     rb.AddRelativeForce(Player.forward);

}

         










}//EC