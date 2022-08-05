using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamOffset : MonoBehaviour
{

    public GameObject Cam1;
    CinemachineFreeLook freeLook;
    CinemachineComposer comp;
    



    // Start is called before the first frame update
    void Start()
    {




       

        freeLook = Cam1.GetComponent<CinemachineFreeLook>();

        comp = freeLook.GetRig(1).GetCinemachineComponent<CinemachineComposer>();

        comp.m_TrackedObjectOffset.x = 1f ;











    }
















}
