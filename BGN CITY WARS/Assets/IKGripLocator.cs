using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class IKGripLocator : MonoBehaviour
{
    public GameObject ActiveWeapon;
    private Transform Grip;
    private AimIK aimik;
    private Transform SECONDARYGRIP;

    // Update is called once per frame


    private void Start()
    {
        aimik = this.GetComponent<AimIK>();
        
    }

    void Update()
    {
        if (ActiveWeapon != null)
        {
            Grip = ActiveWeapon.transform.GetChild(0).transform.Find("SECONDARY GRIP");

        }

        if (Grip != null)
        {

            aimik.solver.target = Grip;
        }




    }


}
