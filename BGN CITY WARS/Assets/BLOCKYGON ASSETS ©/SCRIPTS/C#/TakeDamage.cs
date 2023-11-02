using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class TakeDamage : MonoBehaviour
{
    public int HP = 100;
    public int Shield = 100;
    private PhotonView pv;
    public int LastDamageTook;
    public bool hurt;
    [Header("CanvasEFX")]
    public Transform DamageScreenEFX;

    private void Start()
    {
        int MaxHP = 100;
        HP = MaxHP;
        pv = this.GetComponent<PhotonView>();
    }

    private void Update()
    {
        HPcap();
        SHIELDcap();
    }

    [PunRPC]
    public void Takedamage(int Damage)
    {
        LastDamageTook = Damage;
        hurt = true;
        if (DamageScreenEFX.transform != null)
        {
            DamageScreenEFX.gameObject.SetActive(true);
        }
     

        if (HP > 0 && pv != null)
        {
            if (Shield <= 0f & pv.IsMine)
            {
                HP -= Damage;
            }
            else
            {
                if (Shield < Damage)
                {
                    int remainingDamage = Damage - Shield;
                    Shield = 0;
                    HP -= remainingDamage;
                }
                else
                {
                    Shield -= Damage;
                }
            }
        }
        else
        {
            if (Shield <= 0f)
            {
                HP -= Damage;
            }
            else
            {
                if (Shield < Damage)
                {
                    int remainingDamage = Damage - Shield;
                    Shield = 0;
                    HP -= remainingDamage;
                }
                else
                {
                    Shield -= Damage;
                }
            }
        }
    }

    //when hp goes below 0 it sets back to 0
    private void HPcap()
    {
        if (HP <= 0)
            HP = 0;
    }

    //same but for shield
    private void SHIELDcap()
    {
        if (Shield <= 0)
            Shield = 0;
    }
}
