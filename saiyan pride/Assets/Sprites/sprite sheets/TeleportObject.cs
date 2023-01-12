using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;
    public float distance = 1.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Teleport();
        }
    }

    void Teleport()
    {
        Vector3 objectBPosition = objectB.transform.position;
        Vector3 objectBForward = objectB.transform.forward;
        Vector3 objectAPosition = objectBPosition + objectBForward * -distance;
        objectA.transform.position = objectAPosition;
        objectA.transform.rotation = Quaternion.LookRotation(objectBForward * -1);
    }
}
