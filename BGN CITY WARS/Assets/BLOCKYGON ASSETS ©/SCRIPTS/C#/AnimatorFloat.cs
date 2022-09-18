using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFloat : MonoBehaviour

{

    public float moveV;
    public float moveH;
    public float divide = 15f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
     moveV = Input.GetAxis("Vertical")/ divide;
     moveH = Input.GetAxis("Horizontal")/ divide;
    
     moveH = moveH / 15f;
    animator.SetFloat("inputy", moveV, 1f, Time.deltaTime * 10f);
    animator.SetFloat("inputx", moveH, 1f, Time.deltaTime * 10f);

    }
}
