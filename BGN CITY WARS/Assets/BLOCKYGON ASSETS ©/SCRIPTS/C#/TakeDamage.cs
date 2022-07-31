using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float HP = 1f;
    public float Shield = 1f;

       





        public void Takedamage(float Damage)

    {//sf

        if (Shield <= 0f)
        {
            HP = HP - Damage;
        }

        else
        {
            Shield = Shield - Damage;
        }

                



        
                
        

        
                





    }//ef

























}
