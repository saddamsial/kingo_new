using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTimer : MonoBehaviour
{
     public bool attacking;
    public float speed;
    public bool attacked = false;
    public float timer;
    public int Combo;
    public int Maxcombo = 5;
    public float attackspeed;
    public bool ready =true;
    private float DEFAULTattackspeed;
   
    // Update is called once per frame

    private void Start()
    {
     DEFAULTattackspeed = attackspeed;
    }
    void Update()
    {
        if (Combo > Maxcombo)
        {
        ResetCombo();
        }
              
        if (timer > 0)
        {

         timer -= Time.deltaTime;

        }
        else if (timer < 0)
        {
    
           timer = 0;
           ResetCombo();
        }

         ResetReady();
         //bool method
         if (attacking)

         {
            AddCombo();
         }
    }


    public void AddCombo()
    {       
        if(ready) 
        {
        Combo ++;
        timer =+ speed;
        attacked = true;
        ready = false;

        }
    }

public void ResetCombo()
{
    Combo = 0;
    attacked = false;
}


void ResetReady()

{
    if (!ready)
    {
        
     if (attackspeed > 0)
        {

         attackspeed -= Time.deltaTime;

        }
        else if (attackspeed < 0)
        {
        ready = true;
        attackspeed = DEFAULTattackspeed;
        


    }
}

}




public void AttackingOn()
{
    attacking = true;
}

public void AttackingOff()
{
    attacking = false;
}




}