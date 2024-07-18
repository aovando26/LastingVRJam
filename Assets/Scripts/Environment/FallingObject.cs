using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public BalanceMechanic balanceMechanic;
    public bool isRightSide;
    public float weight = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Add weight to the balance when it hits the player
            balanceMechanic.AddWeight(weight, isRightSide);
            Destroy(gameObject); // Destroy the object after it's caught
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            // Destroy the object if it hits the ground
            Destroy(gameObject);
        }
    }
}