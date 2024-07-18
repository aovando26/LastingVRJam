using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] hurlingPrefabs;
    //private int intervalOfX = 10;
    //private int spawnPosY = 10;
    private float startDelay = 2.0f;
    private float spawnInterval = 1.5f;
    public Transform[] spawnPoints; 

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points assigned");
        }

        InvokeRepeating("SpawningObjects", startDelay, spawnInterval);
    }

    private void SpawningObjects()
    {
        int indexCount = Random.Range(0, hurlingPrefabs.Length);

        //int randomX = Random.Range(-intervalOfX, intervalOfX);

        //Vector3 posPrefabs = new Vector3(randomX, spawnPosY, 0);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(hurlingPrefabs[indexCount], _sp.position, _sp.rotation);
    }
}
