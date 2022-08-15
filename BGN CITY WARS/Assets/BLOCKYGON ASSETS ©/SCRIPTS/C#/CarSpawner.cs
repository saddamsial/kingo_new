using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;

public class CarSpawner : MonoBehaviour

{//SC

//Spawnvars
public Transform VehiclePos;
public GameObject Vehicle;
public Transform Player;


//SpawnableVars
public Vector3 DirectionCheck;
public float MaxDistanceCheck;

public float BlockRadius;



[SerializeField]
private bool Blocked;

//MessageVars
public GameObject MessageError;




public LayerMask layerMask;


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

 
    
    if (Physics.CheckSphere(Player.position,BlockRadius,layerMask))
    {
     Blocked= true;
     

    }

     else 
     {Blocked= false ;
  

}






private void OnDrawGizmos()
{
    Gizmos.DrawWireSphere(Player.position,BlockRadius);
}











void SpawnCar()

{

if (Blocked != true && Input.GetKeyDown(KeyCode.E))
{

PhotonNetwork.Instantiate(Vehicle.name,VehiclePos.position,Quaternion.identity);



  


}

}














}//EC
