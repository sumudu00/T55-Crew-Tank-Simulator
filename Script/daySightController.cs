using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daySightController : MonoBehaviour
{

    //// Reference to the Camera component
    //private Camera cam;

    //// Two different FOVs for the camera
    //private float fov1 = 18f;
    //private float fov2 = 9f;

    //// Use this for initialization
    //void Start()
    //{
    //    // Get the Camera component
    //    cam = GetComponent<Camera>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // Check if the 'Z' key was pressed
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        // If the current FOV is fov1, change it to fov2, and vice versa
    //        if (cam.fieldOfView == fov1)
    //        {
    //            cam.fieldOfView = fov2;
    //        }
    //        else
    //        {
    //            cam.fieldOfView = fov1;
    //        }
    //    }
    //}
    // Reference to the Camera component
    private Camera cam;

    // Two different FOVs for the camera
    private float fov1 = 18f;
    private float fov2 = 9f;

    // Two different focal lengths for the camera
    private float focalLength1 = 95.02196f;
    private float focalLength2 = 191.1284f;

    // Use this for initialization
    void Start()
    {
        // Get the Camera component
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the 'Z' key was pressed
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // If the current FOV is fov1, change it to fov2, and vice versa
            if (cam.fieldOfView == fov1)
            {
                cam.fieldOfView = fov2;
                cam.sensorSize = new Vector2(focalLength2, focalLength1);
            }
            else
            {
                cam.fieldOfView = fov1;
                cam.sensorSize = new Vector2(focalLength1, focalLength2);
            }
        }
    }

}


