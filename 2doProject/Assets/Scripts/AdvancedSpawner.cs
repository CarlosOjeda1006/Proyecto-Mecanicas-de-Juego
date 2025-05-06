using UnityEngine;
using System.Collections.Generic;

public class AdvancedSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public BoxCollider spawnArea;
    public float spawnInterval = 2f;
    public int maxObjects = 20;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("TrySpawn", 0f, spawnInterval);
    }

    void TrySpawn()
    {
        if (spawnedObjects.Count >= maxObjects)
            return;

        SpawnObject();
    }

    void SpawnObject()
    {
        GameObject prefab = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        Vector3 areaSize = spawnArea.size;
        Vector3 areaCenter = spawnArea.center + spawnArea.transform.position;

        Vector3 randomPos = new Vector3(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            Random.Range(-areaSize.y / 2, areaSize.y / 2),
            Random.Range(-areaSize.z / 2, areaSize.z / 2)
        );

        Vector3 spawnPos = areaCenter + randomPos;

        GameObject spawned = Instantiate(prefab, spawnPos, Quaternion.identity);
        spawnedObjects.Add(spawned);

        spawned.AddComponent<DestroyNotifier>().spawner = this;
    }

    public void NotifyDestroyed(GameObject obj)
    {
        spawnedObjects.Remove(obj);
    }
}

public class DestroyNotifier : MonoBehaviour
{
    public AdvancedSpawner spawner;

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.NotifyDestroyed(gameObject);
        }
    }
}

