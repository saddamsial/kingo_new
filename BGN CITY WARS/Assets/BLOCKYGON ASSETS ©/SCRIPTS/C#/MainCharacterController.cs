using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ControlFreak2;



public class MainCharacterController : MonoBehaviour
{
    #region Variables
    [Space(10)]
    [Header("ControollerMode")]
    [Space(3)]
    public bool Combatmode;
    [Space(10)]
    [Header("CombatMode")]
    [Space(3)]
    [SerializeField]
    private float RotateTowardsSpeed;
    [SerializeField]
    private float CMMoveThreshold;
    public float CMSpeed;
    [Space(10)]
    [Header("FreeMode")]
    [Space(3)]
    [SerializeField]
    private float FMMoveThreshold;
    public float FMSpeed;
    public float MoveSmoothness;
    private float Lerp;
    [SerializeField]
    private float FreeRotationSpeed;
    private Quaternion freeRotation;
    public float turnSpeed = 10f;
    //misc
    private PhotonView PV;
    private ControlFreak2.TouchJoystick joystick;
    private CharacterController CharController;
    private Transform MainCamera;
    private Vector3 targetDirection;

    #endregion
    void Start()
    {
        joystick = GameObject.FindWithTag("JoyStick").GetComponent<TouchJoystick>();

        PV = GetComponent<PhotonView>();

        CharController = GetComponent<CharacterController>();

        MainCamera = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (Combatmode)
        {
            CombatMode();
        }
        else
        {
            FreeMode();
        }




   


        void CombatMode()
        {
            //Combat mode features
            #region RotateChar
            Vector3 Rotation = new Vector3(MainCamera.forward.x, 0, MainCamera.forward.z);

            Vector3 SmoothRotation = Vector3.Lerp(transform.forward, Rotation, Time.deltaTime * RotateTowardsSpeed);

            transform.forward = (SmoothRotation);
            #endregion
        }



        void FreeMode()
        {
            #region Free Movement
            if (PV.IsMine && joystick.GetVector().magnitude > FMMoveThreshold)

            {
                // Get the joystick input vector
                Vector3 joystickInput = joystick.GetVector();
                if (Lerp < FMSpeed)
                {
                    Lerp = Mathf.Clamp(Lerp += MoveSmoothness * Time.deltaTime, 0, FMSpeed);
                }




                // Create a movement vector based on the joystick input
                Vector3 movement = transform.rotation * new Vector3(0, 0, Mathf.Abs(joystickInput.magnitude * Lerp * Time.deltaTime));

                // Apply the movement to the character controller
                CharController.Move(movement);
            }
            else
            {
                if (Lerp > 0)
                {
                    Lerp = Mathf.Clamp(Lerp -= MoveSmoothness * 0.5f * Time.deltaTime, 0, FMSpeed);
                }
            }

            #endregion

            #region Free Rotate
            if (joystick.GetVector() != Vector2.zero && targetDirection.magnitude > 0.1f)
            {
                Vector3 lookDirection = targetDirection.normalized;
                freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
                var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
                var eulerY = transform.eulerAngles.y;

                if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
                var euler = new Vector3(0, eulerY, 0);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * FreeRotationSpeed * Time.deltaTime);
            }

            FreeRotationSpeed = 1f;
            var forward = MainCamera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            //get the right-facing direction of the referenceTransform
            var right = MainCamera.transform.TransformDirection(Vector3.right);

            // determine the direction the player will face based on input and the referenceTransform's right and forward directions
            targetDirection = joystick.GetVector().x * right + joystick.GetVector().y * forward;

            #endregion


        }


    }
}



