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
    public float smoothness;


    private void Start()
    {
        vars = GetComponent<PlayerActionsVar>();
       
    }


    void Update()

    {
       
        if (vars.Combat & !vars.IsAiming)

          
        FL.m_Lens.FieldOfView =Mathf.Lerp(0, CMC,Time.deltaTime*smoothness);

      

        else if (vars.IsAiming)
        {
            FL.m_Lens.FieldOfView = Mathf.Lerp(0, AMC, Time.deltaTime * smoothness);
        }

        else
        {
            FL.m_Lens.FieldOfView = Mathf.Lerp(0, FMC, Time.deltaTime * smoothness);


        }

    }



}
