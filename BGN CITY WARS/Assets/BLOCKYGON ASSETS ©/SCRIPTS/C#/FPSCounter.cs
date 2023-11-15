using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public float averagingTime = 2.0f; // Time period for averaging in seconds
    private float fpsAccumulator = 0f;
    private float fpsNextPeriod = 0f;
    private int currentFps;
    private GUIStyle style = new GUIStyle();

    void Start()
    {
        fpsNextPeriod = Time.realtimeSinceStartup + averagingTime;
    }

    void Update()
    {
        // Measure average frames per second
        fpsAccumulator += Time.timeScale / Time.deltaTime;
        ++currentFps;

        // Check if the averaging period has passed
        if (Time.realtimeSinceStartup > fpsNextPeriod)
        {
            // Calculate average FPS
            float fps = fpsAccumulator / currentFps;

            // Log the result
            Debug.Log("Average FPS: " + fps);

            // Reset counters for the next averaging period
            fpsAccumulator = 0f;
            currentFps = 0;
            fpsNextPeriod += averagingTime;
        }
    }

    void OnGUI()
    {
        // Display current FPS on the screen (optional)
        style.normal.textColor = Color.white;
        style.fontSize = 20;
        GUI.Label(new Rect(10, 10, 200, 30), "FPS: " + Mathf.RoundToInt(1f / Time.deltaTime), style);
    }
}
