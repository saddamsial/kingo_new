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
        pv = this.GetComponent<PhotonView>();
        

    }


 private void Update() 
 {
    HPcap();
    SHIELDcap();
    
 }

        [PunRPC]
    public void Takedamage(int Damage)

    {//sf
    LastDamageTook= Damage;
    
     hurt = true;
        DamageScreenEFX.gameObject.SetActive(true);

    if (pv!= null)
    {
   
    
        if (Shield <= 0f & pv.IsMine)
        {
            HP = HP - Damage;
        }

        else
        {
            Shield = Shield - Damage;
        }

                
}

else 
{

        if (Shield <= 0f )
        {
            HP = HP - Damage;
        }

        else
        {
            Shield = Shield - Damage;
        }



}
        
                
        

        
                





    }//ef


//when hp goes bellow 0 it sets back to 0
 private void HPcap()
 {
    if (HP<= 0)
    HP =0;
 }
//same but for shield
 private void SHIELDcap()
 {
    if (Shield<= 0)
    Shield =0;
 }





















}
