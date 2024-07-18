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

        FallingObject fallingObject = spawnedObject.AddComponent<FallingObject>();
        fallingObject.balanceMechanic = balanceMechanic;
        fallingObject.isRightSide = (sp.position.x > 0);
    }
}