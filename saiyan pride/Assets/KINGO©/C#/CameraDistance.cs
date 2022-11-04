using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraDistance : MonoBehaviour
{

    public float Distance;
    public CinemachineVirtualCamera Vcam;
public CinemachineFramingTransposer FT;
    



    void Start()
    {
        Vcam = this.GetComponent<CinemachineVirtualCamera>();

      
    }

    // Update is called once per frame
    void Update()
    {
     FT = Vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
    
    FT.m_CameraDistance = Distance;


    }
}
