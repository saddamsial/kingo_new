using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LootHP : MonoBehaviour
{
    public AudioClip PickupSFX;
    [SerializeField]
    private float RespawnTime;
    void OnTriggerEnter(Collider other)

    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.name + "HAS PICKED UP LOOT");
            PhotonView PV;
            PV = other.GetComponent<PhotonView>();
            PV.RPC("RestoreHP", RpcTarget.All); Debug.Log("Restored HP Over the Network");
            other.GetComponent<AudioSource>().PlayOneShot(PickupSFX);
            PickedUp();
        }
    }
    private void PickedUp()
    {
        transform.parent.parent.GetComponent<ReEnable>().ReEnableItem(RespawnTime, this.gameObject.transform.parent.gameObject);
        transform.parent.gameObject.SetActive(false);
    }

}
