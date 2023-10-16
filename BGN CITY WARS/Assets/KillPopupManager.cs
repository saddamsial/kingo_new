using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class KillPopupManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float DestroyTime;
    [SerializeField]
    private PhotonView PV;
    public string PlayerKiller;
    public string PlayerKilled;

    [SerializeField]
    private TextMeshProUGUI  TXT;
    void Start()
    {
     //   PV = gameObject.GetComponent<PhotonView>();
        Invoke("Destroy", DestroyTime);
        Transform parentTransform = GameObject.Find("KILLS NOTIFICATION").transform;
        transform.SetParent(parentTransform, false);
    }

    // Update is called once per frame
    void Update()
    {

        { TXT.text = PlayerKiller + " " + "Killed" + " " + PlayerKilled; }
   
    }

    void Destroy()
    {
        PhotonNetwork.Destroy(PV.gameObject);
    }
    

}
