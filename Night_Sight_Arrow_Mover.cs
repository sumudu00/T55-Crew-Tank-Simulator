using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night_Sight_Arrow_Mover : MonoBehaviour
{
    public float movespeed;
    public float posY;
    private float upperBound = 180.0f;
    private float lowerBound = -110.0f;

    void Update()
    {
       

        posY = transform.localPosition.y;

        // move graticule upward
        if (Input.GetKey(KeyCode.Keypad8))

        {
            if (posY < upperBound)                          // control the upper limit
            {

                transform.Translate(0, 30 * Time.deltaTime, 0);
            }
            else { posY = 199; }


        }


        // move graticule down

        if (Input.GetKey(KeyCode.Keypad2))
        {

            if (posY > lowerBound)                                              // checked lower lewel 
            {
                transform.Translate(0, -30 * Time.deltaTime, 0);
            }

            else { posY = -80; }





        }




    }
}
