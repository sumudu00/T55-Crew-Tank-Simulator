using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightSightController : MonoBehaviour
{
    private Camera cam;
    //private bool isNightsightEnabled = false;

    // Two different FOVs for the camera
    private float fov1 = 6f;
    private float fov2 = 5.5f;

    // Two different focal lengths for the camera
    //private float focalLength1 = 219.4331f;
    //private float focalLength2 = 239.4165f;

    private Texture2D blackTexture;

    // Use this for initialization
    void Start()
    {
        // Get the Camera component
        cam = GetComponent<Camera>();
        //cam.enabled = false;

        //// Create a black texture
        //blackTexture = new Texture2D(1, 1);
        //blackTexture.SetPixel(0, 0, Color.black);
        //blackTexture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the 'V' key was pressed
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    // Toggle the Nightsight feature
        //    isNightsightEnabled = !isNightsightEnabled;

        //    // If Nightsight is enabled, start rendering
        //    if (isNightsightEnabled)
        //    {
        //        cam.enabled = true;
               
        //    }
        //    // If Nightsight is disabled, stop rendering
        //    else
        //    {
        //        cam.enabled = false;
        //    }

        //    OnGUI();

        //}


        // Check if the 'Z' key was pressed
        if (Input.GetKeyDown(KeyCode.X))
        {
            // If the current FOV is fov1, change it to fov2, and vice versa
            if (cam.fieldOfView == fov1)
            {
                cam.fieldOfView = fov2;
                //cam.sensorSize = new Vector2(focalLength2, focalLength2);
            }
            else
            {
                cam.fieldOfView = fov1;
                //cam.sensorSize = new Vector2(focalLength1, focalLength1);
            }
        }
    }


    //// Draw the black texture when the camera is not rendering
    //void OnGUI()
    //{
    //    if (!cam.enabled)
    //    {
    //        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);
    //    }
    //}
}
