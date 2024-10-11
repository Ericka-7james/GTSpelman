using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRod : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public bool applyFriction = false;  // Toggle for friction fault
    public float frictionAmount = 0.95f; // Rate at which friction reduces speed

    void Update()
    {
        // Rotate the rod along the X-axis or Z-axis (depending on your setup)
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);

        // If friction is applied, reduce the rotation speed gradually
        if (applyFriction && rotationSpeed > 0)
        {
            rotationSpeed *= frictionAmount;  // Reduce speed gradually
        }
    }

    // Method to toggle friction fault on and off
    public void ToggleFriction()
    {
        applyFriction = !applyFriction;
    }
}

