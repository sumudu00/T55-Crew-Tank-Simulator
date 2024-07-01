using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class enableTank : MonoBehaviour
{
 
    //// Array of tanks
    //public GameObject[] tanks;

    //void Start()
    //{
    //    // Get the path of the TankInfo.txt file in the Data folder inside the exe
    //    string filePath = Path.Combine(Application.dataPath, "Data", "TankInfo.txt");

    //    try
    //    {
    //        // Read the tank name from the file
    //        string tankName = File.ReadAllText(filePath).Trim();

    //        // Activate the corresponding tank based on the tank name
    //        ActivateTank(tankName);
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.LogError("Error reading tank name from file: " + e.Message);
    //    }
    //}

    //void ActivateTank(string tankName)
    //{
    //    // Loop through each tank
    //    foreach (GameObject tank in tanks)
    //    {
    //        // If the tank's name matches the tank name read from the file, activate it
    //        if (tank.name == tankName)
    //        {
    //            tank.SetActive(true);
    //            Debug.Log("Activated Tank: " + tankName);
    //        }
    //        else
    //        {
    //            // Deactivate other tanks
    //            tank.SetActive(false);
    //        }
    //    }
    //}


    void Start()
    {
        // Get the path of the TankInfo.txt file
        string filePath = Path.Combine(Application.dataPath, "..", "TankInfo.txt");

        try
        {
            // Read the tank name from the file
            string tankName = File.ReadAllText(filePath).Trim();

            // Print the tank name
            Debug.Log("Tank Name: " + tankName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading tank name from file: " + e.Message);
        }
    }


}


