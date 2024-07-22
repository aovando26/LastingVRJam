using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BalanceMechanic balanceMechanic;
    public bool isRightSide;
    public float weight = 1f;
    private SpawnManager spawnManager;
    private bool isDestroyed = false;

    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        balanceMechanic = FindObjectOfType<BalanceMechanic>();

        if (balanceMechanic == null)
        {
            Debug.LogError("BalanceMechanic component not found in the scene.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDestroyed) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            balanceMechanic.AddWeight(weight, isRightSide);
            DestroyEnemy();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        isDestroyed = true;
        spawnManager.EnemyDestroyed(this);
        Destroy(gameObject);
    }
}