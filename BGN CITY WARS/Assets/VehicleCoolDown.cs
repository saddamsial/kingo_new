using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VehicleCoolDown : MonoBehaviour
{//sc
   
private CarSpawner carSpawner;
[SerializeField]
private GameObject Player;


 private void Start() 
{
   Player =  GameObject.FindGameObjectWithTag("Player") ;
   carSpawner = Player.GetComponent<CarSpawner>();
}







 public IEnumerator Spawncooldown()
{

yield return new WaitForSeconds (carSpawner.SpawnTime);

carSpawner.ReadyToSpawn = true;



}







}//ec
