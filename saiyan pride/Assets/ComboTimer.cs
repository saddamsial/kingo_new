using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTimer : MonoBehaviour
{
    public float speed;
    public bool attacked = false;
    public float timer;
    public int Combo;
    // Update is called once per frame
    void Update()
    {
        if (timer > 0 && attacked)
        {

         timer -= Time.deltaTime;

        }
        else if (timer < 0)
        {
           attacked = false; 
           timer = 0;
        }
    }


    public void AddCombo()
    {
        Combo =+ 1;
    }

void ResetCombo()
{
    Combo = 0;
}

}
