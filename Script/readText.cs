using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class readText : MonoBehaviour
{
    public string tankName;
    public string objectNameToCheck;
    public turret_Elevate turretMove;
    public turret_spin rotateTurret;
    public SpawnBullet SpawnBullet;
    void Start()
    {
        if (File.Exists(Application.dataPath + "/SelectTankText.txt"))
        {
            string[] TextLines = System.IO.File.ReadAllLines(Application.dataPath + "/SelectTankText.txt");

            foreach (string line in TextLines)
            {

                string[] LineArray = line.Split('=');
                if (LineArray[0] == "TankName")
                {
                    tankName = LineArray[1];
                }

                Debug.Log(tankName);





                if (tankName == objectNameToCheck)
                {
                    turretMove.activeTank = true;
                    rotateTurret.activeTank = true; 
                    SpawnBullet.activeTank = true;
                }

            }


        }
    }
}
    
