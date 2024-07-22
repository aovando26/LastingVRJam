using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private int limitY = -5;

    void Start()
    {

    }

    void Update()
    {
        if (transform.position.y < limitY)
        {
            Destroy(gameObject);
        }
    }
}