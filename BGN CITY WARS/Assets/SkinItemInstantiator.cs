using UnityEngine;
using Photon.Pun;

public class SkinItemInstantiator : MonoBehaviourPunCallbacks
{
    public GameObject skinItemPrefab;

    void Awake()
    {
        if (photonView.IsMine)
        {
            // Instantiate the SkinItem object
            GameObject newSkinItem = PhotonNetwork.Instantiate(skinItemPrefab.name, transform.position, Quaternion.identity);
            
            // Find the "SKIN" child transform
            Transform skinTransform = transform.Find("SKIN");

            if (skinTransform != null)
            {
                // Set the new SkinItem as a child of the "SKIN" transform
                newSkinItem.transform.parent = skinTransform;
            }
            else
            {
                Debug.LogError("No 'SKIN' child transform found on the player!");
            }
        }
    }
}
