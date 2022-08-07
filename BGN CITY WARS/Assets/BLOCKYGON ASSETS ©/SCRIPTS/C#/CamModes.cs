using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CamModes : MonoBehaviour
{
    //public CinemachineFreeLook FL;

    public CinemachineVirtualCamera VC;

    private PlayerActionsVar vars;
    //CAM MODES
    public float FMC = 30f;
    public float CMC = 25;
    public float AMC = 21;
    public float smoothness = 40f;
    private float currentspeedFMC;
    private  float currentspeedCMC;
    private float currentspeedAMC;
    public  CinemachineFramingTransposer FT;


    private void Awake()
    {
       

    }
    private void Start()
    {
        vars = GetComponent<PlayerActionsVar>();

      

    }


    void Update()

    {
        CinemachineFramingTransposer FT = VC.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (vars.Combat & !vars.IsAiming)


        VC.m_Lens.FieldOfView = Mathf.SmoothDamp(VC.m_Lens.FieldOfView, CMC, ref currentspeedCMC, Time.deltaTime * smoothness);

      

        else if (vars.IsAiming)
        {
            VC.m_Lens.FieldOfView = Mathf.SmoothDamp(VC.m_Lens.FieldOfView, AMC, ref currentspeedAMC, Time.deltaTime * smoothness);
        }

        else
        {
            VC.m_Lens.FieldOfView = Mathf.SmoothDamp(VC.m_Lens.FieldOfView, FMC, ref currentspeedFMC, Time.deltaTime * smoothness);


        }




        //check camera distance


        if (vars.IsAiming)
        {
       

            FT.m_CameraDistance = 5.5;
            
        }

          else
        {

            FT.m_CameraDistance = 4;
        }










    }






   




}
