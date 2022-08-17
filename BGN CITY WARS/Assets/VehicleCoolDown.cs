using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VehicleCoolDown : MonoBehaviour
{//sc
   
private CarSpawner carSpawner;
[SerializeField]
public GameObject Player;

public Vector3 Ready;



 private void Awake() 
{
  
   carSpawner = Player.GetComponent<CarSpawner>();
}

 private void Update() 

{
     Ready = carSpawner.OffsetCheck;
}





 public IEnumerator Spawncooldown()
{
    Debug.Log("reached coroutine");

yield return new WaitForSeconds (carSpawner.SpawnTime);

//Ready= (true);



}








}//ec
