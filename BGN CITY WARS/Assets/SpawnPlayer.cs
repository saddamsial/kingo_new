using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Transform SpawnPoint;
    [SerializeField]
    private bool AutoSpawn;

    // Start is called before the first frame update
    private void Awake()
    {
        if (AutoSpawn)
        {
            PhotonNetwork.Instantiate(Player.name, SpawnPoint.position, Quaternion.Euler(0, 0, 0));
        }
       
    }


    public void SpawnPlayerManually()
    {
        PhotonNetwork.Instantiate(Player.name, SpawnPoint.position, Quaternion.Euler(0, 0, 0));
    }
}
