using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CamModes : MonoBehaviour
{
    public CinemachineFreeLook FL;
    [SerializeField]
    private PlayerActionsVar vars;
    //CAM MODES
    public float FMC = 30f;
    public float CMC = 25;
    public float AMC = 21;
    public float smoothness = 40f;
    public float currentspeedFMC;
    public float currentspeedCMC;
    public float currentspeedAMC;

    private void Start()
    {
        vars = GetComponent<PlayerActionsVar>();
       
    }


    void Update()

    {
       
        if (vars.Combat & !vars.IsAiming)

          
        FL.m_Lens.FieldOfView = Mathf.SmoothDamp(FL.m_Lens.FieldOfView, CMC, ref currentspeedCMC, Time.deltaTime * smoothness);

      

        else if (vars.IsAiming)
        {
            FL.m_Lens.FieldOfView = Mathf.SmoothDamp(FL.m_Lens.FieldOfView, AMC, ref currentspeedAMC, Time.deltaTime * smoothness);
        }

        else
        {
            FL.m_Lens.FieldOfView = Mathf.SmoothDamp(FL.m_Lens.FieldOfView, FMC, ref currentspeedFMC, Time.deltaTime * smoothness);


        }

    }



}
