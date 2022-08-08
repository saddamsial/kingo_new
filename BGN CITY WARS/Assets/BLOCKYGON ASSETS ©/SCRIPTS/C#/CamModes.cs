using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CamModes : MonoBehaviour
{
    //public CinemachineFreeLook FL;

    public Camera Camera;
   
    private PlayerActionsVar vars;
    //CAM MODES
    public float FMC = 30f;
    public float CMC = 25;
    public float AMC = 21;
    public float smoothness = 40f;
    private float currentspeedFMC;
    private  float currentspeedCMC;
    private float currentspeedAMC;
   
    

    private void Awake()
    {
       Camera = Camera.main;

    }
    private void Start()
    {
        vars = GetComponent<PlayerActionsVar>();

      

    }


    void Update()

    {
      //  CinemachineFramingTransposer FT = VC.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (vars.Combat & !vars.IsAiming)


            Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, CMC, ref currentspeedCMC, Time.deltaTime * smoothness);






        else if (vars.IsAiming)
        {
            Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, AMC, ref currentspeedAMC, Time.deltaTime * smoothness);
        }

        else
        {
            Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, FMC, ref currentspeedFMC, Time.deltaTime * smoothness);


        }




        //check camera distance


     //   if (vars.IsAiming)
      //  {
       

        //    FT.m_ScreenX = xAMC;
            
        }
    
       //  else if (vars.Combat & !vars.IsAiming)
    //   {

        //    FT.m_ScreenX = xCMC;
    //  }

    //   else
      //  {
      //      FT.m_ScreenX = xFMC;
     //   }
    









  //  }






   




}
