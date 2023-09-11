using Photon.Pun;
using UnityEngine;
using System.IO;

public class PhotonTransformSerializer : MonoBehaviour, IPunObservable
{
    public float smoothingFactor = 0.1f; // Public variable for smoothing factor

    private PhotonView photonView; // Reference to the GameObject's Photon View

    private void Start()
    {
        // Get the PhotonView component on this GameObject (if it exists)
        photonView = GetComponent<PhotonView>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && photonView != null)
        {
            // We own this player: send local data to others
            stream.SendNext(transform.localPosition);
            stream.SendNext(transform.localRotation);
        }
        else if (photonView != null)
        {
            // Network player, receive data
            Vector3 receivedLocalPosition = (Vector3)stream.ReceiveNext();
            Quaternion receivedLocalRotation = (Quaternion)stream.ReceiveNext();

            // Smoothly interpolate local position and rotation
            transform.localPosition = Vector3.Lerp(transform.localPosition, receivedLocalPosition, smoothingFactor);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, receivedLocalRotation, smoothingFactor);
        }
    }
}
