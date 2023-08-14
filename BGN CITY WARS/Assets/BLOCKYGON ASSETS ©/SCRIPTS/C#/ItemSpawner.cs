using UnityEngine;
using Photon.Pun;
using System.IO;

public class ItemSpawner : MonoBehaviour
{
    public void SpawnItemFromID(int id, Vector3 localPosition, Quaternion localRotation, string itemPath, Transform parent = null, Vector3 scaleTo = default)
    {
        string prefabName = "Item_" + id.ToString(); // Create the prefab name based on the ID.

        // Combine the itemPath and prefabName using Path.Combine
        string concatenatedPath = Path.Combine(itemPath,prefabName);

        // Load the prefab from the "Resources" folder using the concatenated path.
        GameObject spawnempty = Resources.Load<GameObject>("spawnempty");
        GameObject itemPrefab = Resources.Load<GameObject>(concatenatedPath);

        if (itemPrefab != null)
        { 
         
            // Instantiate the prefab over the network at the specified local position and rotation relative to the parent.
            GameObject spawnedItem = PhotonNetwork.Instantiate(itemPrefab.name, localPosition, localRotation);

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
