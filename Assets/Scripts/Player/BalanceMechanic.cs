using UnityEngine;
using UnityEngine.SceneManagement;

public class BalanceMechanic : MonoBehaviour
{
    public float maxTiltAngle = 45f; // Maximum tilt angle before falling
    public float balanceSpeed = 2f; // Speed of balance adjustment
    public float fallThreshold = 0.8f; // Percentage of max tilt that causes falling
    public float resetDelay = 2f; // Delay before resetting the scene

    private float currentBalance = 0f; // Current balance state (-1 to 1)
    private bool hasFallen = false;

    // Reference to the tightrope walker's transform
    public Transform tightropeWalker;

    private void Update()
    {
        if (hasFallen) return;

        // Update the tightrope walker's rotation based on the current balance
        float tiltAngle = currentBalance * maxTiltAngle;
        tightropeWalker.rotation = Quaternion.Euler(0, 0, tiltAngle);

        // Check if the player has fallen
        if (Mathf.Abs(currentBalance) > fallThreshold)
        {
            Fall();
        }
    }

    public void AddWeight(float weight, bool isRightSide)
    {
        if (hasFallen) return;

        // Adjust the balance based on the added weight and side
        float balanceChange = weight * (isRightSide ? 1 : -1) * balanceSpeed * Time.deltaTime;
        currentBalance = Mathf.Clamp(currentBalance + balanceChange, -1f, 1f);
    }

    private void Fall()
    {
        hasFallen = true;
        Debug.Log("Player has fallen! Resetting scene in " + resetDelay + " seconds.");

        // Implement fall animation here if desired

        // Schedule the scene reset
        Invoke("ResetScene", resetDelay);
    }

    private void ResetScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to reset the balance (can be called when restarting the game without reloading the scene)
    public void ResetBalance()
    {
        currentBalance = 0f;
        hasFallen = false;
        tightropeWalker.rotation = Quaternion.identity;
    }
}