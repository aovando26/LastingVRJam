using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField]
    private float windForce = 10.0f;  // Default wind force

    private void OnTriggerStay(Collider other)
    {
        // Get the GameObject that entered the trigger
        var hitObj = other.gameObject;

        // Check if the GameObject has a Rigidbody component
        var rb = hitObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply force in the Z-axis direction
            Vector3 forceDirection = transform.forward;
            rb.AddForce(forceDirection * windForce);
        }
    }
}
