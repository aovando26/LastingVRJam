using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to spawn
    public Transform[] spawnPoints; // Array of spawn points

    // A reference to the WaveManager instance
    private WaveManager waveManager;

    // Responsible for spawning enemies
    private Coroutine spawnCoroutine;

    // List to keep track of currently active enemies
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        waveManager = WaveManager.Instance;
        if (waveManager == null)
        {
            Debug.LogError("WaveManager not found in the scene!");
            return;
        }

        // Subscribe to wave start and end events
        waveManager.OnWaveStart += StartSpawning;
        waveManager.OnWaveEnd += StopSpawning;
    }

    // Starts the coroutine to spawn enemies
    void StartSpawning(int waveNumber)
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    // Stops the coroutine that spawns enemies
    void StopSpawning(int waveNumber)
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    // Coroutine that continuously spawns enemies based on the spawn interval defined by WaveManager
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (waveManager.CanSpawnEnemy())
            {
                SpawnRandomEnemy();
                yield return new WaitForSeconds(waveManager.GetCurrentSpawnInterval());
            }
            else
            {
                yield return null; // Wait for the next frame if we can't spawn
            }
        }
    }

    // Spawns a random enemy at a random spawn point
    void SpawnRandomEnemy()
    {
        int indexCount = Random.Range(0, enemyPrefabs.Length);
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newEnemy = Instantiate(enemyPrefabs[indexCount], sp.position, sp.rotation);

        // Add enemy to active list
        activeEnemies.Add(newEnemy);
    }

    // Handles the destruction of an enemy
    public void EnemyDestroyed(Enemy enemy)
    {
        activeEnemies.Remove(enemy.gameObject);
        waveManager.EnemyKilled();
    }

    // Handles the event of the SpawnManager being destroyed
    void OnDestroy()
    {
        if (waveManager != null)
        {
            waveManager.OnWaveStart -= StartSpawning;
            waveManager.OnWaveEnd -= StopSpawning;
        }
    }
}