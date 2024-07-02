using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SelectTank : MonoBehaviour
{
    public string tankName;
    public Wheel_Control_CS TankControlScript;

    // Start is called before the first frame update
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

                
            }
        }

        if (this.gameObject.name == tankName)
        {
            TankControlScript.ActivateTank = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
