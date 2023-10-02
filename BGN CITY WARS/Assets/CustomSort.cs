using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSort : MonoBehaviour
{
    public Transform content; // Reference to the content panel in your scroll view.

    private void Update()
    {
        // Get all the UI objects under the content panel.
        List<Transform> childList = new List<Transform>();
        foreach (Transform child in content)
        {
            childList.Add(child);
        }

        // Sort the objects based on their integer values (replace "name" with your property).
        childList.Sort((a, b) =>
        {
            int aValue = int.Parse(a.name); // Replace with your property.
            int bValue = int.Parse(b.name); // Replace with your property.
            return aValue.CompareTo(bValue);
        });

        // Reorder the UI objects in the content panel.
        foreach (Transform child in childList)
        {
            child.SetAsLastSibling();
        }
    }
}
