using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testthrow : MonoBehaviour
{
    private Rigidbody rb;
    public float Force;
    void OnEnable()
    {
      rb = transform.GetChild(0).GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
        rb.AddForce( transform.GetChild(0).transform.forward*Force);
        }
    }
}
