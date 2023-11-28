using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkDestroy : MonoBehaviour
{
    [SerializeField]
    private bool AutoDestroy;
    [SerializeField]
    private float destructionDelay;
    // Start is called before the first frame update
    void Start()
    {
        if (AutoDestroy)
        {
            Invoke("AutoDestroyObject", destructionDelay);
        }
    }


    void AutoDestroyObject()
    {
        PhotonNetwork.Destroy(transform.gameObject);
    }


    public void DestroyManually()
    {
        PhotonNetwork.Destroy(transform.gameObject);
    }

}
