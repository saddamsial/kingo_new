using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;

public class CarSpawner : MonoBehaviour

{//SC

//Spawnvars
public Transform VehiclePos;
public GameObject VehicleToSpawn;
[SerializeField]
private GameObject VehichleSpawned;

public Transform Player;
public Vector3 OffsetCheck;



//SpawnableVarslge

public float BlockRadius;



[SerializeField]
private bool Blocked;

//MessageVars
public GameObject MessageError;
public KeyCode Keybind;




public LayerMask layerMask;

//Timer for respawn
public float SpawnTime=1f;
public bool ReadyToCool = false;
public bool ReadyToSpawn = true;
[SerializeField]
private bool IsSpawned = false;



//level script access
[SerializeField]
private VehicleCoolDown vehicleCoolDown;



//set Player
void Start()
{
    Player = this.transform;

    vehicleCoolDown = GameObject.FindGameObjectWithTag("Level Script").GetComponent<VehicleCoolDown>();
    vehicleCoolDown.Player = this.gameObject;
    
}




 void FixedUpdate() 
 
 {
   
MessageError.SetActive(Blocked);

    CheckSpawnable();
     
    SpawnCar();

    


 }





void CheckSpawnable()
{

 
    
    if (Physics.CheckSphere(Player.position + OffsetCheck + transform.forward,BlockRadius,layerMask))
    
       {Blocked= true;
     
       }
    

     else 
     {Blocked= false ;
     }

}






private void OnDrawGizmos()
{
    Gizmos.DrawWireSphere(Player.position + OffsetCheck + transform.forward,BlockRadius);
}











void SpawnCar()

{
 
if (Blocked != true && Input.GetKeyDown(Keybind) & ReadyToSpawn)
{
    
ReadyToSpawn = false;

ReadyToCool = true;

     


if (IsSpawned)
{
PhotonNetwork.Destroy(VehichleSpawned);

VehichleSpawned = PhotonNetwork.Instantiate(VehicleToSpawn.name,VehiclePos.position,VehiclePos.rotation);
IsSpawned= true;
}

else

 {
VehichleSpawned = PhotonNetwork.Instantiate(VehicleToSpawn.name,VehiclePos.position,VehiclePos.rotation);
IsSpawned=true;
 }




  



}

}










}//EC
