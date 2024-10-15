using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class moveTank : MonoBehaviour
{
    [Header("Tank Movement Settings")]
    public float maxSpeed = 10f;  // Maximum speed in m/s
    public float acceleration = 5f;  // Acceleration speed
    public float brakeForce = 10f;  // Brake force
    public float turnSpeed = 45f;  // Turning speed in degrees per second
    public float clutchSensitivity = 0.1f;  // Sensitivity of clutch release

    [Header("Ground Settings")]
    public float raycastDistance = 2f;  // Distance to detect the ground below the tank
    public LayerMask groundLayer;  // Define the layer for the ground
    public float tankHeightOffset = 0.5f;  // How far above the ground the tank should be

    private Rigidbody rb;
    private float currentSpeed = 0f;  // Current tank speed
    private float inputThrottle = 0f;  // Throttle input (forward/backward)
    private float inputSteer = 0f;  // Steering input (left/right)
    private bool isBraking = false;  // Is the brake applied
    private bool isClutchPressed = false;  // Is the clutch pressed
    private bool isHandBrake = false;  // Handbrake state

    [Header("Key Bindings")]
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode brakeKey = KeyCode.Space;
    public KeyCode clutchKey = KeyCode.LeftShift;
    public KeyCode handBrakeKey = KeyCode.LeftControl;

    private Vector3 groundNormal = Vector3.up;  // Ground normal direction

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        HandleClutch();
        HandleMovement();
        HandleSteering();
        HandleBrakes();
        GroundTank();  // Ensure the tank stays grounded
    }

    private void GetInput()
    {
        inputThrottle = 0f;

        if (Input.GetKey(forwardKey)) inputThrottle = 1f;
        if (Input.GetKey(backwardKey)) inputThrottle = -1f;

        inputSteer = 0f;
        if (Input.GetKey(leftKey)) inputSteer = -1f;
        if (Input.GetKey(rightKey)) inputSteer = 1f;

        isBraking = Input.GetKey(brakeKey);
        isClutchPressed = Input.GetKey(clutchKey);
        isHandBrake = Input.GetKey(handBrakeKey);
    }

    private void HandleClutch()
    {
        if (isClutchPressed)
        {
            // Slow down acceleration and reduce throttle sensitivity while clutch is pressed.
            inputThrottle *= clutchSensitivity;
        }
    }

    private void HandleMovement()
    {
        // Apply acceleration and cap speed.
        currentSpeed += inputThrottle * acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        // Calculate forward movement along the ground
        Vector3 forwardMovement = Vector3.ProjectOnPlane(transform.forward, groundNormal) * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }

    private void HandleSteering()
    {
        // Apply tank turning based on input.
        float rotationAngle = inputSteer * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, rotationAngle, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void HandleBrakes()
    {
        if (isBraking)
        {
            // Apply braking by reducing speed faster.
            currentSpeed -= brakeForce * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        }

        if (isHandBrake)
        {
            // Freeze the tank's movement.
            currentSpeed = 0f;
        }
    }

    private void GroundTank()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        // Perform raycast to detect the ground below the tank
        if (Physics.Raycast(ray, out hit, raycastDistance, groundLayer))
        {
            // Snap the tank to the ground with a small height offset to prevent sinking into terrain
            Vector3 correctedPosition = hit.point + Vector3.up * tankHeightOffset;

            // Clamp the Y position to ensure it does not exceed 5
            correctedPosition.y = Mathf.Min(correctedPosition.y, 5f);

            rb.MovePosition(correctedPosition);

            // Update the ground normal to align movement with the terrain
            groundNormal = hit.normal;
        }
        else
        {
            // Apply gravity if no ground is detected (optional, depending on your physics setup)
            rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);

            // Ensure the Y position doesn't exceed 5 even when in free fall
            Vector3 clampedPosition = rb.position;
            clampedPosition.y = Mathf.Min(clampedPosition.y, 1f);
            rb.MovePosition(clampedPosition);
        }
    }   
}

