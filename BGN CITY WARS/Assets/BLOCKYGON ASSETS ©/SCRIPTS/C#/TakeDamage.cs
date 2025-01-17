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
    [SerializeField]
    private bool MaxHPStart;
    public bool hurt;
    [SerializeField]
    private UIBarRefresh Refreshbar;
    [Header("Event")]
    public Transform ActivateEvent;

    private void Start()
    {
        int MaxHP = 100;
        if(MaxHPStart)
        {
            HP = MaxHP;
        }
     
        pv = this.GetComponent<PhotonView>();
    }


    [PunRPC]
    public void Takedamage(int Damage)
    {
        LastDamageTook = Damage;
        hurt = true;
        if (ActivateEvent.transform != null)
        {
            ActivateEvent.gameObject.SetActive(true);
        }
     

        if (HP > 0 && pv != null)
        {
            if (Shield <= 0f & pv.IsMine)
            {
                HP -= Damage;   Refreshbar.UpdateHP(HP);
            }
            else
            {
                if (Shield < Damage)
                {
                    int remainingDamage = Damage - Shield;
                    Shield = 0;
                    HP -= remainingDamage;           HPcap();  if (Refreshbar != null) { Refreshbar.UpdateHP(HP); }; 
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
                HP -= Damage;                            HPcap(); if (Refreshbar != null) { Refreshbar.UpdateHP(HP); };
            }
            else
            {
                if (Shield < Damage)
                {
                    int remainingDamage = Damage - Shield;
                    Shield = 0;
                    HP -= remainingDamage;       HPcap(); if (Refreshbar != null) { Refreshbar.UpdateHP(HP); };
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
    [PunRPC]
    public void RestoreHP()
    {
        HP = 100; if (Refreshbar != null) { Refreshbar.UpdateHP(HP); };
    }
}
