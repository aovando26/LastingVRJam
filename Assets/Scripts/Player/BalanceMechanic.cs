using UnityEngine;
using UnityEngine.SceneManagement;

public class BalanceMechanic : MonoBehaviour
{
    public float maxTiltAngle = 45f;
    public float balanceSpeed = 2f;
    public float fallThreshold = 0.8f;
    public float resetDelay = 2f;

    private float currentBalance = 0f;
    private bool hasFallen = false;

    public Transform tightropeWalker;

    private void Update()
    {
        if (hasFallen) return;

        float tiltAngle = currentBalance * maxTiltAngle;
        tightropeWalker.rotation = Quaternion.Euler(0, 0, tiltAngle);

        if (Mathf.Abs(currentBalance) > fallThreshold)
        {
            Fall();
        }
    }

    public void AddWeight(float weight, bool isRightSide)
    {
        if (hasFallen) return;

        float balanceChange = weight * (isRightSide ? 1 : -1) * balanceSpeed * Time.deltaTime;
        currentBalance = Mathf.Clamp(currentBalance + balanceChange, -1f, 1f);
    }

    private void Fall()
    {
        hasFallen = true;
        Debug.Log("Player has fallen! Resetting scene in " + resetDelay + " seconds.");

        Invoke("ResetScene", resetDelay);
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetBalance()
    {
        currentBalance = 0f;
        hasFallen = false;
        tightropeWalker.rotation = Quaternion.identity;
    }
}