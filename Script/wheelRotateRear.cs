using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelRotateRear : MonoBehaviour
{
   
    public float rotationSpeed = 100.0f;

    void Update()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Rotate around Y-axis based on horizontal input
        //float rotationY = moveHorizontal * rotationSpeed * Time.deltaTime;

        // Rotate around X-axis based on vertical input
        float rotationX = moveVertical * -rotationSpeed * Time.deltaTime;

        transform.Rotate(/*rotationY*/0, rotationX, 0);
    }
}
