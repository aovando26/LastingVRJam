using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField]
    private float windForce = 10.0f;  // default wind force

    private void OnTriggerStay(Collider other)
    {
        // get the GameObject that entered the trigger
        var hitObj = other.gameObject;

        // retreiving rigidbody
        var rb = hitObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // applying force on negative z-axis direction
            Vector3 forceDirection = -transform.forward;
            rb.AddForce(forceDirection * windForce);
        }
    }
}
