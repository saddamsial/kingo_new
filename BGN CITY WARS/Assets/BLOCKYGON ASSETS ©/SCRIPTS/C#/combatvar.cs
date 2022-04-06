using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class combatvar : MonoBehaviour
{

    public bool combat;
    public GameObject FreeLook;
    public GameObject Combatlook;
    public Animator Animator;

    void Update()
    {
        if (combat == true)
        {
            Combatlook.SetActive(true);
            FreeLook.SetActive(false);
        }
        else
        {
            Combatlook.SetActive(false);
            FreeLook.SetActive(true);

        }


        ////animator set
        Animator.SetBool("combat", combat);
        
        }


    

}
