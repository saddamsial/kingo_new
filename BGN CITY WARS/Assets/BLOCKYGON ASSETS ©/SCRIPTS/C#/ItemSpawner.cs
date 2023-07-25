using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject spawnedItem;
 
    public void SpawnItemFromID(int id, Vector3 localPosition, Quaternion localRotation, Transform parent = null, Vector3 scaleTo = default)
    {
        string prefabName = "Item_" + id.ToString(); // Create the prefab name based on the ID.

        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/" + prefabName); // Load the prefab from Resources folder.

        if (itemPrefab != null)
        {
            // Instantiate the prefab at the specified local position and rotation relative to the parent.
            spawnedItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

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
            Debug.LogError("Prefab with name " + prefabName + " not found!");
        }
    }
}
