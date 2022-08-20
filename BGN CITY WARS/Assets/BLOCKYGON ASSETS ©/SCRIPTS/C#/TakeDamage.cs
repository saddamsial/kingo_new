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
   public bool hurt;
    private void Start()
    {
        pv = this.GetComponent<PhotonView>();

    }


 private void Update() 
 {
    HPcap();
    SHIELDcap();
 }


    public void Takedamage(float Damage)

    {//sf
    
     hurt = true;

    if(pv!= null)
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
    HP =0F;
 }
//same but for shield
 private void SHIELDcap()
 {
    if (Shield<= 0)
    Shield =0F;
 }





















}
