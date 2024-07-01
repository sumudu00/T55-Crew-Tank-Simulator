using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateWheel : MonoBehaviour


{

    public float rotationSpeed = 100.0f;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Rotate around Y-axis based on horizontal input
        float rotationY = moveHorizontal * rotationSpeed * Time.deltaTime;

        // Rotate around X-axis based on vertical input
        float rotationX = moveVertical * rotationSpeed * Time.deltaTime;

        transform.Rotate(rotationY, rotationX, 0);
    }






    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        // Rotate forward
    //        float rotation = rotationSpeed * Time.deltaTime;
    //        transform.Rotate(rotation, 0, 0);
    //    }
    //    else if (Input.GetKey(KeyCode.S))
    //    {
    //        // Rotate backward
    //        float rotation = -rotationSpeed * Time.deltaTime;
    //        transform.Rotate(rotation, 0, 0);
    //    }
    //}




    //public GameObject[] wheels; // Array to hold wheel game objects
    //public float speed = 1f; // Speed of the vehicle
    //public float wheelRotationSpeed = 10f; // Speed at which wheels rotate

    //private void Update()
    //{
    //    // Get user input
    //    float moveInput = 0f;

    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        moveInput = 1f;
    //    }
    //    else if (Input.GetKey(KeyCode.S))
    //    {
    //        moveInput = -1f;
    //    }

    //    // Move the vehicle forward or backward
    //    Vector3 moveDirection = transform.forward * moveInput * speed * Time.deltaTime;
    //    transform.Rotate(moveDirection, Space.World);

    //    // Rotate the wheels based on the movement
    //    RotateWheels(moveInput);
    //}

    //private void RotateWheels(float moveInput)
    //{
    //    // Calculate the rotation angle based on the movement input
    //    float rotationAngle = moveInput * wheelRotationSpeed * Time.deltaTime;

    //    // Rotate each wheel
    //    foreach (GameObject wheel in wheels)
    //    {
    //        wheel.transform.Rotate(Vector3.right, rotationAngle);
    //    }
    //}
}




    






