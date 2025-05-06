using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;

    public float timeToSpawn;
    private float spawnCounter;

    public int maxEnemies = -1;

    private int currentEnemies = 0;

    void Start()
    {
        spawnCounter = timeToSpawn;
    }

    void Update()
    {
        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0)
        {
            spawnCounter = timeToSpawn;

            if (maxEnemies <= 0 || currentEnemies < maxEnemies)
            {
                GameObject spawnedEnemy = Instantiate(enemyToSpawn, transform.position, transform.rotation);
                currentEnemies++;

                spawnedEnemy.AddComponent<EnemyTracker>().spawner = this;
            }
        }
    }
    public void NotifyEnemyDestroyed()
    {
        currentEnemies--;
    }
}

public class EnemyTracker : MonoBehaviour
{
    public EnemySpawner spawner;

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.NotifyEnemyDestroyed();
        }
    }
}

