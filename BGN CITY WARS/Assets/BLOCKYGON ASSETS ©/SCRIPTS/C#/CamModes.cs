using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamModes : MonoBehaviour
{
    public Camera Camera;
    public PlayerActionsVar vars;
    //CAM MODES
    public float FMC = 30f;
    public float CMC = 25;
    public float AMC = 21;
     public float SprintingFOV = 40f;
     public float SprintDamp = 40f;
    public float smoothness = 40f;
    public float smoothnessAMC = 20f;
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
      
        if (!vars.Sprinting)
        {
        if (vars.Combat & !vars.IsAiming)


            Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, CMC, ref currentspeedCMC, Time.deltaTime * smoothness);






        else if (vars.IsAiming)
        {
            Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, AMC, ref currentspeedAMC, Time.deltaTime * smoothnessAMC);
        }

        else
        {
            Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, FMC, ref currentspeedFMC, Time.deltaTime * smoothness);


        }


        }
        else SprintfOV();

}
    void SprintfOV()
    {
     if(vars.Sprinting)
    {

     Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, SprintingFOV, ref currentspeedCMC, Time.deltaTime * SprintDamp);
     
    }   

    }





}
