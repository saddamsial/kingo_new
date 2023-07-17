using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimate : MonoBehaviour
{
    private Animator animator;
    public string TriggerName;
    void Start()
    {
        animator= this.GetComponent<Animator>();
    }
    public void Animate()
     {
        animator.SetTrigger(TriggerName);
     }
}
