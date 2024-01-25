using UnityEngine;
using Photon.Pun;

public class ApplicationManager : MonoBehaviour {

	[SerializeField]
	private int GameFrameRate;
	[SerializeField]
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

		PhotonNetwork.GameVersion = Application.version;
		Debug.Log("AppVersion" + PhotonNetwork.GameVersion);
	



	}
}
