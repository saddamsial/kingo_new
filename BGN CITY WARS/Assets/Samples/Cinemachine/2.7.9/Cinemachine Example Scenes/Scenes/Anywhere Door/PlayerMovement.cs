using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
public class PlayerMovement : MonoBehaviour
{
public float movementSpeed = 10f;
public float lookatspeed = 5f;
public PhotonView PV;



    public void Start()
    {
        PV = GetComponent<PhotonView>();

    }













    void Update () 
    {
        if (PV.IsMine)
        {
            Controller();
        }
     
        }


    void Controller()
    {

        if (Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey("s"))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }

        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }

        //mouse look at
        float horizontal = Input.GetAxis("Mouse X") * lookatspeed;
        float vertical = Input.GetAxis("Mouse Y") * lookatspeed;

        transform.Rotate(0f, horizontal, 0f, Space.World);
        //transform.Rotate(-vertical, 0f, 0f, Space.Self);
    }













}
