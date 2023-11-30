using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour {

	[SerializeField]
	private int GameFrameRate;
	[SerializeField]
	private int SleepTimeout;
	[SerializeField]
	private int brightness;
	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

    private void Start()
    {
		Application.targetFrameRate = GameFrameRate; //set FPS

		Screen.sleepTimeout = SleepTimeout;
		Screen.brightness = brightness;


	}
}
