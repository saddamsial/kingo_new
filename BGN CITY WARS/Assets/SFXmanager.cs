using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SFXmanager : MonoBehaviour
{
    private AudioSource AS;
    private PhotonView PV;

    [SerializeField]
    private AudioClip[] StepClips;

    [SerializeField]
    private AudioClip[] punchClips;


    private void Start()
    {
        AS = this.GetComponent<AudioSource>();
        PV = this.GetComponent<PhotonView>();


        #region SpatialBlend Specify
        if (PV.IsMine)
        {
            AS.spatialBlend = 0;
        }
        else AS.spatialBlend = 1;

        #endregion spatial blend 

    }
    #region AnimationEventFunctions
    public void PlayRandomPunchSFX()
    {
        if (punchClips.Length > 0)
        {
            // Choose a random index from the array
            int randomIndex = Random.Range(0, punchClips.Length);

            // Play the selected punch sound
            AudioSource.PlayClipAtPoint(punchClips[randomIndex], transform.position);
        }
        else
        {
            Debug.LogError("No punch clips available in the array.");
        }
    }

    public void PlayRandomStepSFX()
    {
        if (punchClips.Length > 0)
        {
            // Choose a random index from the array
            int randomIndex = Random.Range(0, StepClips.Length);

            // Play the selected punch sound
            AudioSource.PlayClipAtPoint(StepClips[randomIndex], transform.position);
        }
        else
        {
            Debug.LogError("No StepClips clips available in the array.");
        }
    }


    #endregion


}



