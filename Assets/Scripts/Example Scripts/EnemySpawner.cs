using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public float spawnRate = 3f;
    public Transform[] spawnPoints;

    private float nextSpawnTime = 0f;
    bool bossSpawned = false;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
        if (GameManagerEx.Instance.score > 400 && GameManagerEx.Instance.score < 900)
            spawnRate = 1.0f;
        if (GameManagerEx.Instance.score > 900 && GameManagerEx.Instance.score < 1400)
            spawnRate = 0.5f;
        if (GameManagerEx.Instance.score > 1400 && GameManagerEx.Instance.score < 2000)
            spawnRate = 0.25f;
        if (bossSpawned == false && GameManagerEx.Instance.score > 1400 && GameManagerEx.Instance.score < 2000)
        {
            bossSpawned = true;
            SpawnBoss();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab && spawnPoints.Length > 0)
        {
            // Check if game is still running through Singleton
            if (GameManagerEx.Instance.lives > 0)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length - 1);
                Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
            }
        }
    }

    private void SpawnBoss()
    {
        if (bossPrefab && spawnPoints.Length > 0)
        {
            if (GameManagerEx.Instance.lives > 0)
            {
                int bossIndex = 2;
                Instantiate(bossPrefab, spawnPoints[bossIndex].position, Quaternion.identity);
            }
        }
    }
}