using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.UI;

public class RotateRodController : MonoBehaviour
{
    public float baseRotationSpeed = 100f;
    public float currentSpeed;

    public Text speedText;           // Text UI to show the current speed
    public Text faultText;           // Text UI to display faults
    public Slider frictionSlider;    // Slider to control friction
    public Slider misalignmentSlider; // Slider to control misalignment

    public Transform disk1; // Reference to the first disk
    public Transform disk2; // Reference to the second disk

    private float maxMisalignment = 0.2f; // Maximum allowable misalignment (wobble)
    private float frictionThreshold = 30f; // Speed below which a friction fault is detected
    private float misalignmentThreshold = 0.15f; // Misalignment value above which a fault is detected

    void Start()
    {
        currentSpeed = baseRotationSpeed;
        UpdateUI();
    }

    void Update()
    {
        // Adjust speed based on friction slider
        float frictionFactor = 1 - (frictionSlider.value / frictionSlider.maxValue); // Scale friction (0 to 1)
        currentSpeed = baseRotationSpeed * frictionFactor;

        // Apply rotation with friction considered
        transform.Rotate(Vector3.up * currentSpeed * Time.deltaTime);

        // Apply misalignment effect
        float misalignmentFactor = misalignmentSlider.value / misalignmentSlider.maxValue; // Scale misalignment (0 to 1)
        ApplyMisalignment(misalignmentFactor);

        // Perform fault detection
        DetectFaults(frictionFactor, misalignmentFactor);

        // Update UI
        UpdateUI();
    }

    void ApplyMisalignment(float misalignmentFactor)
    {
        // Wobble the disks to simulate misalignment
        float wobble = Mathf.Sin(Time.time * currentSpeed) * maxMisalignment * misalignmentFactor;
        disk1.localPosition = new Vector3(wobble, disk1.localPosition.y, disk1.localPosition.z);
        disk2.localPosition = new Vector3(-wobble, disk2.localPosition.y, disk2.localPosition.z);
    }

    void DetectFaults(float frictionFactor, float misalignmentFactor)
    {
        bool frictionFault = currentSpeed < frictionThreshold;
        bool misalignmentFault = misalignmentFactor > misalignmentThreshold;

        // Explanation for user on why a fault is detected
        string faultMessage = "";

        if (frictionFault)
        {
            faultMessage += "Fault Detected: High Friction! \n";
            faultMessage += "Friction has reduced the speed of the rig significantly, which may cause the system to operate inefficiently.\n";
        }
        if (misalignmentFault)
        {
            faultMessage += "Fault Detected: Misalignment. \n";
            faultMessage += "The rig is wobbling due to misalignment, which can lead to mechanical failure over time.\n";
        }

        // If no faults, show a 'No Fault' message
        if (string.IsNullOrEmpty(faultMessage))
        {
            faultText.text = "No Fault Detected: The rig is operating normally.";
        }
        else
        {
            faultText.text = faultMessage;
        }
    }

    void UpdateUI()
    {
        if (speedText != null)
        {
            speedText.text = "Speed: " + currentSpeed.ToString("F2");
        }
    }
}
