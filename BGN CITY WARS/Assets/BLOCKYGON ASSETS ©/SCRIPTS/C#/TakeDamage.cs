using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class TakeDamage : MonoBehaviour
{
    public float HP = 1f;
    public float Shield = 1f;
    private PhotonView pv;
    private void Start()
    {
        pv = this.GetComponent<PhotonView>();

    }





    public void Takedamage(float Damage)

    {//sf

        if (Shield <= 0f & pv.IsMine)
        {
            HP = HP - Damage;
        }

        else
        {
            Shield = Shield - Damage;
        }

                



        
                
        

        
                





    }//ef

























}
