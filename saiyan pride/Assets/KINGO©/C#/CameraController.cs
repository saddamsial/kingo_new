using UnityEngine;
public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform opponent;
    public float minOrthoSize = 5f;
    public float maxOrthoSize = 10f;
    public float minFOV = 20f;
    public float maxFOV = 60f;
    public float smoothness = 5f;
    public float zoomSpeed = 5f;
    public float shakeMagnitude = 0.05f; // how much to shake the camera
    public float shakeDuration = 0.1f; // how long to shake the camera
    private bool isShaking = false;
    private Vector3 originalPosition; // remember the original position of the camera
    private Quaternion originalRotation; // remember the original rotation of the camera

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

   private void LateUpdate()
{
    Vector3 center = (player.position + opponent.position) / 2f;

    // Calculate the distance between the player and opponent
    float distance = Vector3.Distance(player.position, opponent.position);

    // Calculate the size or field of view of the camera based on the distance and mode
    if (cam.orthographic)
    {
        float targetSize = Mathf.Clamp(distance * zoomSpeed, minOrthoSize, maxOrthoSize);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, smoothness * Time.deltaTime);
    }
    else
    {
        float targetFOV = Mathf.Clamp(distance * zoomSpeed, minFOV, maxFOV);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, smoothness * Time.deltaTime);
    }

    // Smoothly adjust the position of the camera towards the center of the players
    transform.position = Vector3.Lerp(transform.position, new Vector3(center.x, center.y, transform.position.z), smoothness * Time.deltaTime);
     if (isShaking)
    {
        // randomly shake the camera by rotating it around its pivot point within a certain magnitude
        transform.rotation = originalRotation * Quaternion.Euler(Random.Range(-shakeMagnitude, shakeMagnitude), Random.Range(-shakeMagnitude, shakeMagnitude), Random.Range(-shakeMagnitude, shakeMagnitude));

        // decrement the remaining shake duration
        shakeDuration -= Time.unscaledDeltaTime;

        // stop shaking when the duration is up
        if (shakeDuration <= 0f)
        {
            isShaking = false;
            transform.rotation = originalRotation;
        }
    }
    if (Input.GetKeyDown(KeyCode.Space))
    {
        ShakeCamera();
    }
}

public void ShakeCamera()
{
    if (!isShaking)
    {
        isShaking = true;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Calculate the random offset for the camera position and rotation based on the magnitude
    Vector3 positionOffset = new Vector3(Random.Range(-shakeMagnitude, shakeMagnitude), Random.Range(-shakeMagnitude, shakeMagnitude), 0f);
    Quaternion rotationOffset = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-shakeMagnitude, shakeMagnitude)));

    // Apply the offset to the camera position and rotation
    transform.position = originalPosition + positionOffset;
    transform.rotation = originalRotation * rotationOffset;

    // Decrement the remaining shake duration based on the unscaled time elapsed
    shakeDuration -= Time.unscaledDeltaTime;

    // Stop shaking when the duration is up
    if (shakeDuration <= 0f)
    {
        isShaking = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}



}
