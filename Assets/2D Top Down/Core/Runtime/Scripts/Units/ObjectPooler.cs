using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject objectPooled;
    private List<GameObject> _pooledObjects = new List<GameObject>();

    public static ObjectPooler Create(GameObject objectToPool)
    {
        GameObject poolerObj = new GameObject(objectToPool.name + " Pooler");
        ObjectPooler pooler = poolerObj.AddComponent<ObjectPooler>();
        pooler.objectPooled = objectToPool;

        return pooler;
    }

    public void Init(int initialSize)
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject objectSpawned = Instantiate(objectPooled);
            _pooledObjects.Add(objectSpawned);
            objectSpawned.SetActive(false);
        }
    }

    public GameObject SpawnObject(Vector3 position, Quaternion rotation)
    {
        GameObject objectSpawned = null;
        objectSpawned = _pooledObjects.Find(x => !x.activeInHierarchy);

        if (objectSpawned == null)
        {
            objectSpawned = Instantiate(objectPooled, position, rotation);
            _pooledObjects.Add(objectSpawned);

            return objectSpawned;
        }

        objectSpawned.transform.position = position;
        objectSpawned.transform.rotation = rotation;
        objectSpawned.SetActive(true);

        return objectSpawned;
    }
}

