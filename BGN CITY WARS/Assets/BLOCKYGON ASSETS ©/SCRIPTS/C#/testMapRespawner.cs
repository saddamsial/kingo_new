using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMapRespawner : MonoBehaviour
{
  private GameObject Player;
  public List<Transform> spawnpoints;
  public float TeleportTime = 2f;
  [SerializeField]
  private bool ready = true;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       if ( Player.GetComponent<TakeDamage>().HP <= 0 && ready)
       {
        StartCoroutine(Respawn());
       }
       




 IEnumerator Respawn()
{
       ready = false;
yield return new WaitForSeconds (TeleportTime);


   Player.transform.position = spawnpoints[Random.Range(0,4)].transform.position;

  ready = true;
}


}
}