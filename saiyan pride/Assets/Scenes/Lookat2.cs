using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat2 : MonoBehaviour
{
    public Transform objectToRotateTowards;
    float currentVelocity = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = objectToRotateTowards.transform.position - transform.position;
        float wantedAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, wantedAngle, ref currentVelocity, 0.5f);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
