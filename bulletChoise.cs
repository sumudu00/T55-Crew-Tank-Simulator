using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class bulletChoise : MonoBehaviour
{
    Rigidbody rb;
    public bool bulletHe = false;
    public bool bulletHeat = false;
    public GameObject spawnItem1 = null;                 //spawn item
    public GameObject spawnItem2 = null;                 //spawn item
    public Transform spawnPoint = null;                 //spawn position

    public float velocitykmph = 800f;      //800            //spawn VELOCITY
    public float velocitymps;

    public bool loaded = false;
    public bool activeTank = false;
    private ParticleSystem particleSystem1;
    private ParticleSystem particleSystem2;
    float lastFireTime = 0f;
    // Define the cooldown duration in seconds
    float fireCooldown = 2f;

    void Start()
    {
        particleSystem1 = GameObject.Find("Particle_Fire").GetComponent<ParticleSystem>();
        particleSystem2 = GameObject.Find("Particle_Smoke").GetComponent<ParticleSystem>();
    }


    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        bulletHe = true;

    //    }
    //    if (Input.GetKey(KeyCode.Q))
    //    {
    //        bulletHeat = true;

    //    }
    //    if (Input.GetKey(KeyCode.R))
    //    {
    //        loaded = true;
    //    }


    //    if (Input.GetKey(KeyCode.F))
    //    {
    //        velocitymps = velocitykmph * 1000f / 3600f;
    //        if (loaded)
    //        {
    //            if (bulletHe)
    //            {
    //                Debug.Log("bulletHe");
    //                GameObject bullet = Instantiate(spawnItem1, spawnPoint.position, spawnPoint.rotation);      //Spawning Object
    //                rb = bullet.GetComponent<Rigidbody>();
    //                rb.AddForce(spawnPoint.forward * velocitymps, ForceMode.VelocityChange);
    //                loaded = false;
    //                particleSystem1.Play();
    //                particleSystem2.Play();
    //            }
    //            else if (bulletHeat) 
    //            {
    //                Debug.Log("bulletHeat");
    //                GameObject bullet = Instantiate(spawnItem2, spawnPoint.position, spawnPoint.rotation);      //Spawning Object
    //                rb = bullet.GetComponent<Rigidbody>();
    //                rb.AddForce(spawnPoint.forward * velocitymps/2, ForceMode.VelocityChange);
    //                loaded = false;
    //                particleSystem1.Play();
    //                particleSystem2.Play();
    //            }

    //        }
    //    }
    //}
    void Update()
    {
        // Check if Q key is pressed to select HEAT ammo
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bulletHe = false;
            bulletHeat = true;
            Debug.Log("Selected HEAT ammo");
        }

        // Check if E key is pressed to select HE ammo
        if (Input.GetKeyDown(KeyCode.E))
        {
            bulletHe = true;
            bulletHeat = false;
            Debug.Log("Selected HE ammo");
        }

        // Check if R key is pressed to load the selected ammo
        if (Input.GetKeyDown(KeyCode.R))
        {
            loaded = true;
            Debug.Log("Ammo loaded");
        }

        // Check if F key is pressed to fire the selected ammo
        if (Input.GetKeyDown(KeyCode.F))
        {
            velocitymps = velocitykmph * 1000f / 3600f;
            if (loaded && Time.time - lastFireTime >= fireCooldown)
            {
                if (bulletHe)
                {
                    Debug.Log("Firing HE");
                    GameObject bullet = Instantiate(spawnItem1, spawnPoint.position, spawnPoint.rotation);
                    rb = bullet.GetComponent<Rigidbody>();
                    rb.AddForce(spawnPoint.forward * velocitymps, ForceMode.VelocityChange);
                    loaded = false;
                    particleSystem1.Play();
                    particleSystem2.Play();
                }
                else if (bulletHeat)
                {
                    Debug.Log("Firing HEAT");
                    GameObject bullet = Instantiate(spawnItem2, spawnPoint.position, spawnPoint.rotation);
                    rb = bullet.GetComponent<Rigidbody>();
                    rb.AddForce(spawnPoint.forward * velocitymps * 2, ForceMode.VelocityChange);
                    loaded = false;
                    particleSystem1.Play();
                    particleSystem2.Play();
                }

                lastFireTime = Time.time;
            }

            else
            {
                Debug.Log("No ammo loaded");

            }
            
        }
    }

}
