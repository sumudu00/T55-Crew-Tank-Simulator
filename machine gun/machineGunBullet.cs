using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class machineGunBullet : MonoBehaviour
{
    public GameObject impactParticlePrefab; // Reference to the "fire_particle" prefab
    public float bulletLifeTime = 3f; // Time before the bullet is destroyed

    void Start()
    {
        // Automatically destroy the bullet after its lifetime
        Destroy(gameObject, bulletLifeTime);
    }

    // Called when the bullet collides with another object
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object is terrain or any other object you want the particle effect on
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("Target"))
        {
            // Instantiate the impact particle system at the collision point
            if (impactParticlePrefab != null)
            {
                // Get the point and normal of collision to spawn the particle effect correctly
                ContactPoint contact = collision.contacts[0];
                GameObject impactEffect = Instantiate(impactParticlePrefab, contact.point, Quaternion.LookRotation(contact.normal));

                // Get the duration of the particle system
                ParticleSystem ps = impactEffect.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    Destroy(impactEffect, ps.main.duration + ps.main.startLifetime.constantMax); // Destroy after the particle finishes
                }
                else
                {
                    // Fallback: Destroy after a delay if the particle system is missing
                    Destroy(impactEffect, 2f); // Assume 2 seconds if no particle system found
                }
            }

            // Destroy the bullet after collision
            Destroy(gameObject);
        }
    }
}
