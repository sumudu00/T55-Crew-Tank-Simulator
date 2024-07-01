using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class commandeNightVision : MonoBehaviour
{
    public GameObject commanderDaySight;
    public GameObject nightSight;
    //public GameObject shutter;
    //public Color shutterColor = new Color(0, 0, 0, 1); // Full black
    //public Color nightSightColor = new Color(25f / 255f, 177f / 255f, 65f / 255f, 1); // RGB values for 19B141

    //private Image shutterImage;

    void Start()
    {

        commanderDaySight.SetActive(true);

        nightSight.SetActive(false);
    }

    void Update()
    {
        // Check if the B key is pressed
        if (Input.GetKeyDown(KeyCode.C) )
        {
            // Toggle the active state of the commander day sight
            commanderDaySight.SetActive(!commanderDaySight.activeSelf);
            // Toggle the active state of the night sight
            nightSight.SetActive(!nightSight.activeSelf);
        }
    }

    //void Start()
    //{
    //    // Ensure the commander day sight is enabled by default
    //    commanderDaySight.SetActive(true);
    //    // Ensure the night sight is disabled by default
    //    nightSight.SetActive(false);

    //    // Get the Image component of the shutter object
    //    shutterImage = shutter.GetComponent<Image>();
    //    if (shutterImage != null)
    //    {
    //        // Set the initial color of the shutter
    //        shutterImage.color = shutterColor;
    //    }
    //}

    //void Update()
    //{
    //    // Check if the C key is pressed to change the color
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        // Change the color of the shutter to nightSightColor
    //        ChangeShutterColor(nightSightColor);
    //    }

    //    // Check if the B key is pressed and the shutter is not active
    //    if (Input.GetKeyDown(KeyCode.B) && shutterImage.color == nightSightColor)
    //    {
    //        // Toggle the active state of the commander day sight
    //        commanderDaySight.SetActive(!commanderDaySight.activeSelf);
    //        // Toggle the active state of the night sight
    //        nightSight.SetActive(!nightSight.activeSelf);
    //    }
    //}

    // Method to change the color of the shutter
    //void ChangeShutterColor(Color newColor)
    //{
    //    if (shutterImage != null)
    //    {
    //        shutterImage.color = newColor;
    //    }
    //}
}
