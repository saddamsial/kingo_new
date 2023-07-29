using UnityEngine;

public class SpeedCheck : MonoBehaviour
{
    public CharacterController characterController;
    public float speed;

    private Vector3 lastPosition;
    private float lastTime;

    private void Start()
    {
        // Initialize the last position and time
        lastPosition = characterController.transform.position;
        lastTime = Time.time;
    }

    private void Update()
    {
        // Calculate the speed based on position changes over time
        Vector3 currentPosition = characterController.transform.position;
        float currentTime = Time.time;
        float deltaTime = currentTime - lastTime;

        // Calculate the distance traveled
        float distance = Vector3.Distance(currentPosition, lastPosition);

        // Calculate speed as distance divided by time
        speed = distance / deltaTime;

        // Update last position and time for the next frame
        lastPosition = currentPosition;
        lastTime = currentTime;
    }
}
