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
            balanceMechanic.AddWeight(weight, isRightSide);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}