using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeControl : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Reference to the camera2 script (make sure the camera2 script is attached to your Main Camera)
    public camera2 cameraController;

    // Threshold to detect swipe
    public float swipeThreshold = 50f;

    // Start position of the swipe
    private Vector2 swipeStartPos;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Store the start position of the swipe when the pointer is pressed
        swipeStartPos = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // This method is needed to implement the IBeginDragHandler interface, but we don't need to do anything here.
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Check if there's no overlapping UI element handling the swipe
        if (!EventSystem.current.IsPointerOverGameObject(eventData.pointerId))
        {
            // Calculate the distance of the swipe
            Vector2 delta = eventData.position - swipeStartPos;

            // Convert the swipe distance to camera rotation values
            float xRotation = -delta.y * cameraController.touchYSensitivity;
            float yRotation = delta.x * cameraController.touchXSensitivity;

            // Apply the rotation to the camera controller
            cameraController.RotateCamera(xRotation, yRotation);

            // Debug the touch input and rotation values
            Debug.Log($"Touch Delta: {delta}, xRotation: {xRotation}, yRotation: {yRotation}");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset the swipe start position when the drag ends
        swipeStartPos = Vector2.zero;
    }
}
