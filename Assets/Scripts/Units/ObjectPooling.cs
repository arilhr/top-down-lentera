using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject objectToPool;
    public List<GameObject> pooledObjects = new List<GameObject>();

    public GameObject SpawnObject(Transform _transform)
    {
        GameObject objectSpawned = null;
        objectSpawned = pooledObjects.Find(x => x.activeInHierarchy);

        if (objectSpawned == null)
        {
            objectSpawned = Instantiate(objectToPool, _transform.position, _transform.rotation);
            pooledObjects.Add(objectSpawned);
            return objectSpawned;
        }

        objectSpawned.SetActive(true);

        return objectSpawned;
    }
}

