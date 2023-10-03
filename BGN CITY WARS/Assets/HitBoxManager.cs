using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HitBoxManager : MonoBehaviour
{
    public List<Transform> HitBoxes;
    [PunRPC]
    public void DeactivateHitBoxes()
    {

        foreach (Transform transform  in HitBoxes)
        {
            transform.gameObject.SetActive(false);
        }

    }
    [PunRPC]
    public void ActivateHitBoxes()
    {

        foreach (Transform transform in HitBoxes)
        {
            transform.gameObject.SetActive(true);
        }

    }







}
