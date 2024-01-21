using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReEnable : MonoBehaviour
{

    private GameObject Objecttoactivate;



    public void ReEnableItem(float Time,GameObject item)
    {
        Objecttoactivate = item;
        Invoke("Countdown", Time);

    }

   void Countdown()
    {
        Objecttoactivate.SetActive(true);

    }

}
