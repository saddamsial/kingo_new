using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public GameObject objectToPool; // The object we want to pool
    public int poolSize = 10; // The initial size of the pool
    public bool canGrow = true; // Can the pool grow if all objects are in use?

    private List<GameObject> pooledObjects; // The list of pooled objects

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObject = Instantiate(objectToPool);
            newObject.SetActive(false);
            pooledObjects.Add(newObject);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (canGrow)
        {
            GameObject newObject = Instantiate(objectToPool);
            pooledObjects.Add(newObject);
            return newObject;
        }

        return null;
    }
}
