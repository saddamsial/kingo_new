using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VehicleCoolDown : MonoBehaviour
{//sc
   
private CarSpawner carSpawner;
[HideInInspector]
public GameObject Player;

private bool TargetActive;



 void Start() 
{
    if(!this.GetComponent<PhotonView>().IsMine)
    {
      this.gameObject.SetActive(false);
    }
}



 private void Update() 

{
     carSpawner = Player.GetComponent<CarSpawner>();
     TargetActive = carSpawner.isActiveAndEnabled;
   
if (carSpawner.ReadyToCool)
 { StartCoroutine (Spawncooldown());




}
  


}





 public IEnumerator Spawncooldown()
{
    Debug.Log("reached coroutine");
 carSpawner.ReadyToCool = (false);
yield return new WaitForSeconds (carSpawner.SpawnTime);

Debug.Log("reached if");
if (!TargetActive)
{
yield return new WaitUntil (() => TargetActive == true );

carSpawner.ReadyToSpawn= (true);
carSpawner.ReadyToCool = (false);

}

else 
{
    carSpawner.ReadyToSpawn= (true);
   
}


}








}//ec
