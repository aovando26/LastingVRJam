using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] hurlingPrefabs;
    private float startDelay = 2.0f;
    private float spawnInterval = 1.5f;
    public Transform[] spawnPoints;
    public BalanceMechanic balanceMechanic;

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
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject spawnedObject = Instantiate(hurlingPrefabs[indexCount], sp.position, sp.rotation);

        // adding script to instantiated object
        FallingObject fallingObject = spawnedObject.AddComponent<FallingObject>();

        // method assigned and applied
        fallingObject.balanceMechanic = balanceMechanic;

        // falling direction
        fallingObject.isRightSide = (sp.position.x > 0);
    }
}