using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFloat : MonoBehaviour

{

    public float moveV;
    public float moveH;
    private float divide = 8f;
    public float SpeedCheckvar;
    private Animator animator;
    public float SyncSpeed = 2f;
    public Vector2 joystick;
    // Start is called before the first frame update
    void Start()
    {





 animator = this.GetComponent<Animator>();



        
    }

    // Update is called once per frame
    void Update()
    {
        
     //moveV = Input.GetAxis("Vertical") * SpeedCheckvar/ divide;
     //moveH = Input.GetAxis("Horizontal")* SpeedCheckvar/ divide;
     moveV =joystick.y /SyncSpeed;
     moveH= joystick.x/SyncSpeed;
     
    animator.SetFloat("inputy", moveV, 1f, Time.deltaTime * 10f);
    animator.SetFloat("inputx", moveH, 1f, Time.deltaTime * 10f);
   

    }
}
