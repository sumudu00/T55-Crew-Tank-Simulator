using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bullet : MonoBehaviour
{
    public Vector3 spawnPoint;
    

    void Start()
    {
        // Set the spawn point to the initial position of the bullet
        spawnPoint = transform.position;
    }

    void Update()
    {
        // Calculate the distance between the current position and the spawn point
        float distance = Vector3.Distance(transform.position, spawnPoint);

        // Print the distance to the console
        Debug.Log("The distance between the spawn point and the dropped point is: " + distance + " units");

       
    }
}









