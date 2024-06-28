using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line_mover : MonoBehaviour
{
    public float movespeed;
    public float posY;
    public float angle;
    private float upperBound = -210.0f;
    private float lowerBound = 180.0f;

    void Update()
    {
        //angle = FindObjectOfType<turret_Elevate>().rotationX;
        //movespeed = 5 * FindObjectOfType<turret_Elevate>().barrelelevateSpeed;

        posY = transform.localPosition.y;
      
        // move graticule down
            if (Input.GetKey(KeyCode.N))

            {
               if (posY < lowerBound)                          // control the lower limit
                {

                    transform.Translate(0, 30 * Time.deltaTime, 0);
                }
                else { posY = 179; }
                

            }

          
            // move graticule upward

            if (Input.GetKey(KeyCode.M))
            {

                if (posY > upperBound)                                              // checked upper lewel 
                {
                    transform.Translate(0, -30 * Time.deltaTime, 0);
                }

                else { posY = -209; }





            }

        


    }
}