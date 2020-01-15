using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectTracker : MonoBehaviour
{
    private Vector3 startPos;
    private Rigidbody objRb;
    private float negativeYTrigger = -20f;

    private void Start()
    {
        objRb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= negativeYTrigger)
        {
            Debug.LogWarning(this.name + " somehow fell below -20y and has respawned to its starting position.");
            StopMovement();
            transform.position = startPos;
        }
    }

    private void StopMovement()
    {
        objRb.velocity = Vector3.zero;
    }
}
