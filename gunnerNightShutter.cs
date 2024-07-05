using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunnerNightShutter : MonoBehaviour
{
    public RawImage rawImage;

    // Flag to keep track of whether the image is currently enabled or disabled
    private bool isImageEnabled = true;

    

    void Start()
    {
        // Get the RawImage component if not assigned
        if (rawImage == null)
        {
            rawImage = GetComponent<RawImage>();
        }

        // Ensure the RawImage is initially enabled
        EnableImage();
    }

    void Update()
    {
        // Check for the "V" key press
        if (Input.GetKeyDown(KeyCode.V))
        {
            // Toggle the image visibility
            if (isImageEnabled)
                DisableImage();
            else
                EnableImage();
        }
    }

    // Method to enable the RawImage
    private void EnableImage()
    {
        //rawImage.enabled = true;
       
        isImageEnabled = true;
    }

    // Method to disable the RawImage
    private void DisableImage()
    {
        rawImage.enabled = false;
        isImageEnabled = false;
    }
}


