using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class activeManager : MonoBehaviour
{
   
    private const string tanksFolder = "Tanks";
    private const string tankFileNamePrefix = "tank";
    private const int numberOfTanks = 3;

    public static int GetActiveTank()
    {
        string activeTankFileName = GetActiveTankFileName();
        if (activeTankFileName == null)
        {
            Console.WriteLine("Error: No active tank file found.");
            return -1;
        }

        string tankNumberStr = activeTankFileName.Substring(tankFileNamePrefix.Length);
        if (!int.TryParse(tankNumberStr, out int activeTankNumber))
        {
            Console.WriteLine("Error: Invalid tank file name format.");
            return -1;
        }

        return activeTankNumber;
    }

    private static string GetActiveTankFileName()
    {
        for (int i = 1; i <= numberOfTanks; i++)
        {
            string tankFileName = $"{tankFileNamePrefix}{i}.txt";
            string tankFilePath = Path.Combine(tanksFolder, tankFileName);
            if (File.Exists(tankFilePath))
            {
                return tankFileName;
            }
        }
        return null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int activeTank = activeManager.GetActiveTank();
        if (activeTank != -1)
        {
            Console.WriteLine($"Active Tank: Tank {activeTank}");
            // Your game code here to use the active tank
        }
    }
}


