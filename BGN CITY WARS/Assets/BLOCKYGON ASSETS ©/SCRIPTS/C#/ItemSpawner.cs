using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public void SpawnItemFromID(int id, Vector3 position, Quaternion rotation, Transform parent = null, Vector3 scaleTo = default)
    {
        string prefabName = "Item_" + id.ToString(); // Create the prefab name based on the ID.

        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/" + prefabName); // Load the prefab from Resources folder.

        if (itemPrefab != null)
        {
            // Instantiate the prefab at the specified position and rotation.
            GameObject spawnedItem = Instantiate(itemPrefab, position, rotation);

            // Set the parent if provided.
            if (parent != null)
            {
                spawnedItem.transform.SetParent(parent);
            }

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
