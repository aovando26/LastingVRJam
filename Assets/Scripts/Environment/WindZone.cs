using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField]
    private float initialWindForce = 10.0f;  // initial wind force
    private float windForce;  // current wind force

    private void Start()
    {
        windForce = initialWindForce;

        // subscribe to wave start events
        WaveManager.Instance.OnWaveStart += OnWaveStart;
    }

    private void OnDestroy()
    {
        // unsubscribe from wave start events
        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.OnWaveStart -= OnWaveStart;
        }
    }

    private void OnWaveStart(int waveNumber)
    {
        // increase wind force by 10 with each wave
        windForce = initialWindForce + (waveNumber - 1) * 10.0f;
        Debug.Log($"Wind force increased to {windForce} for wave {waveNumber}");
    }

    private void OnTriggerStay(Collider other)
    {
        // get the GameObject that entered the trigger
        var hitObj = other.gameObject;

        // retrieve Rigidbody
        var rb = hitObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // apply force in the negative z-axis direction
            Vector3 forceDirection = -transform.forward;
            rb.AddForce(forceDirection * windForce);
        }
    }
}