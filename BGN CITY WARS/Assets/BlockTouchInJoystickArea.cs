using UnityEngine;
using UnityEngine.EventSystems;

public class BlockTouchInJoystickArea : MonoBehaviour, IPointerDownHandler
{
    public RectTransform joystickArea;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Check if the touch position is inside the joystick area
        if (RectTransformUtility.RectangleContainsScreenPoint(joystickArea, eventData.position))
        {
            // Block the touch input if it's inside the joystick area
            eventData.pointerPress = null;
            eventData.pointerDrag = null;
        }
    }
}
