using UnityEngine;

namespace Invector.vCharacterController
{
    public class Vinput : MonoBehaviour
    {
        #region Variables       

    

        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

     
        [HideInInspector] public camera2 tpCamera;
        [HideInInspector] public Camera cameraMain;

        #endregion

        protected virtual void Start()
        {
           
            InitializeTpCamera();
        }

      

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
          
        }

    
        #region Basic Locomotion Inputs

    
        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<camera2>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
         
            CameraInput();
          
           
          
        }


        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                
                }
            }

            if (cameraMain)
            {
               
            }

            if (tpCamera == null)
                return;

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);
        }

  
       
        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
   
        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
       
    

        #endregion       
    }
}