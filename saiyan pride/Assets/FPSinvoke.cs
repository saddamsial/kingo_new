using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSinvoke : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // Invoke ("fps",1);
       Application.targetFrameRate = 60;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
