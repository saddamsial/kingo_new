using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform player;
    public Transform opponent;
    public float smoothTime = 0.3f;
    public float maxZoom = 10f;
    public float minZoom = 5f;
    public float zoomSpeed = 5f;
    public float zoomThreshold = 2f;
    public float buffer = 1.5f;

    private Vector3 velocity = Vector3.zero;

    private void Update() {
        // Calculate the distance between the player and opponent
        float distance = Vector3.Distance(player.position, opponent.position);

        // Calculate the midpoint between the player and opponent
        Vector3 midpoint = (player.position + opponent.position) / 2f;

        // Set the camera's target position based on the midpoint
        Vector3 targetPosition = new Vector3(midpoint.x, midpoint.y, transform.position.z);

        // Calculate the new camera zoom level based on the distance
        float zoomLevel = Mathf.Clamp((distance - zoomThreshold) / zoomSpeed, minZoom, maxZoom);

        // Smoothly move the camera to the new position and zoom level
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomLevel, Time.deltaTime * smoothTime);

        // Check if opponent is within view
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        if (opponent.position.x < bottomLeft.x + buffer || opponent.position.x > topRight.x - buffer ||
            opponent.position.y < bottomLeft.y + buffer || opponent.position.y > topRight.y - buffer) {
            // Adjust camera position to keep opponent within view
            Vector3 clampedOpponentPos = new Vector3(
                Mathf.Clamp(opponent.position.x, bottomLeft.x + buffer, topRight.x - buffer),
                Mathf.Clamp(opponent.position.y, bottomLeft.y + buffer, topRight.y - buffer),
                opponent.position.z
            );
            transform.position = Vector3.SmoothDamp(transform.position, clampedOpponentPos, ref velocity, smoothTime);
        }
    }
}
