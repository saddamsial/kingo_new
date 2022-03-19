using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControls : MonoBehaviour
{
    Vector3 eulerAngleVelocity;
    private Rigidbody RB;
    public float Speed = 15;
    GameObject saftyRing;

    // Start is called before the first frame update
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody>(); // get the RB
        //saftyRing = GetComponent<SpringJoint>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.W)) {

            //GetComponent<Rigidbody>().AddForce(transform.forward * 10000);
            //GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + new Vector3(-1, 0, 0));

            Vector3 forward = transform.forward;
            forward.y = 0f;
            forward.Normalize();
            RB.AddForce(forward * 1 * Speed, ForceMode.Acceleration); // add force forward based on input and horsepower
            RB.AddRelativeTorque(-Vector3.right * 1, ForceMode.Acceleration);

        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 forward = transform.forward;
            forward.y = 0f;
            forward.Normalize();
            RB.AddForce(forward * -1 * Speed, ForceMode.Acceleration); // add force forward based on input and horsepower
            RB.AddRelativeTorque(-Vector3.right * 1, ForceMode.Acceleration);

        }
        if (Input.GetKey(KeyCode.D))
        {
            //eulerAngleVelocity = new Vector3(0, 2000 * Time.deltaTime, 0);
            //Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
            //GetComponent<Rigidbody>().MoveRotation(deltaRotation * GetComponent<Rigidbody>().rotation);

            RB.AddTorque(transform.up * 10000);

            //transform.Rotate(new Vector3(0, 20 * Time.deltaTime, 0));
            //transform.RotateAround(transform.position, transform.up, 40 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            RB.AddTorque(transform.up * -10000);
            //transform.Rotate(new Vector3(0, -20 * Time.deltaTime, 0));
            //transform.RotateAround(transform.position, transform.up, -40 * Time.deltaTime);
        }

        if (RB.velocity.magnitude <= 0.7f)
        {

            // saftyRing.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(saftyRing.GetComponent<Rigidbody>().velocity, 0.4f);

        }
        //saftyRing.GetComponent<Rigidbody>().angularVelocity = Vector3.ClampMagnitude(saftyRing.GetComponent<Rigidbody>().angularVelocity, 1f);

    }
}
