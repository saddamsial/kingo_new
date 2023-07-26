using UnityEngine;
using Photon.Pun;

public class ItemSpawner : MonoBehaviour
{
    public void SpawnItemFromID(int id, Vector3 localPosition, Quaternion localRotation, Transform parent = null, Vector3 scaleTo = default)
    {
        string prefabName = "Item_" + id.ToString(); // Create the prefab name based on the ID.

        // Load the prefab from the "Resources" folder directly using the prefabName.
        GameObject itemPrefab = Resources.Load<GameObject>(prefabName);

        if (itemPrefab != null)
        {
            // Instantiate the prefab over the network at the specified local position and rotation relative to the parent.
            GameObject spawnedItem = PhotonNetwork.Instantiate(itemPrefab.name, Vector3.zero, Quaternion.identity);

            // Set the parent if provided.
            if (parent != null)
            {
                spawnedItem.transform.SetParent(parent);
            }

            // Set the local position and rotation.
            spawnedItem.transform.localPosition = localPosition;
            spawnedItem.transform.localRotation = localRotation;

            // Set the scale if provided.
            if (scaleTo != default)
            {
                spawnedItem.transform.localScale = scaleTo;
            }
        }
        else
        {
            Debug.LogError("Prefab with name " + prefabName + " not found in the 'Resources' folder!");
        }
    }
}
