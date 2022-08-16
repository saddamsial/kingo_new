using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VehicleCoolDown : MonoBehaviour
{//sc
   
private CarSpawner carSpawner;
[SerializeField]
public GameObject Player;


 private void Start() 
{
  
   carSpawner = Player.GetComponent<CarSpawner>();
}







 public IEnumerator Spawncooldown()
{
    Debug.Log("reached coroutine");

yield return new WaitForSeconds (carSpawner.SpawnTime);

carSpawner.ReadyToSpawn = (true);



}







}//ec
