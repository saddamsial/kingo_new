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


//SpawnableVars

public float BlockRadius;



[SerializeField]
private bool Blocked;

//MessageVars
public GameObject MessageError;




public LayerMask layerMask;

//Timer for respawn
public float SpawnTime=1f;
[SerializeField]
private bool ReadyToSpawn = true;
[SerializeField]
private bool IsSpawned = false;




//set Player
void Start()
{
    Player = this.transform;
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
 
if (Blocked != true && Input.GetKeyDown(KeyCode.E) && ReadyToSpawn)
{
if (IsSpawned)
{
PhotonNetwork.Destroy(VehichleSpawned);

VehichleSpawned = PhotonNetwork.Instantiate(VehicleToSpawn.name,VehiclePos.position,Quaternion.identity);
IsSpawned= true;
}

else

 {
VehichleSpawned = PhotonNetwork.Instantiate(VehicleToSpawn.name,VehiclePos.position,Quaternion.identity);
IsSpawned=true;
 }


ReadyToSpawn = false;
  
StartCoroutine (Spawncooldown());

}

}





 IEnumerator Spawncooldown()
{

yield return new WaitForSeconds (SpawnTime);

ReadyToSpawn = true;



}








}//EC
