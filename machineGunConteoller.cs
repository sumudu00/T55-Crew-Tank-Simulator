using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machineGunConteoller : MonoBehaviour
{
    public Transform gunBase; // The base of the machine gun (for left/right rotation)
    public Transform gunBarrel; // The barrel of the machine gun (for up/down rotation)
    public GameObject bulletPrefab; // The bullet prefab
    public Transform firePoint; // Where the bullet will spawn
    public float rotationSpeed = 50f; // Speed of the gun rotation
    public float fireRate = 10f; // Bullets per second
    public float bulletForce = 1000f; // Force applied to bullets
    public GameObject hitEffectPrefab; // Particle system prefab for bullet impact
    public float bulletLifeTime = 3f; // Bullet lifetime in seconds
    private ParticleSystem particleSystem1;

    private bool isFiring = false;
    private float nextFireTime = 0f;

    private void Start()
    {
        particleSystem1 = GameObject.Find("VFX_MuzzleFlash").GetComponent<ParticleSystem>();

        particleSystem1.Stop();
    }

    void Update()
    {
        // Handle gun rotation
        RotateGun();

        // Handle firing
        if (Input.GetKey(KeyCode.Keypad5))
        {
            if (!isFiring)
            {
                StartCoroutine(FireGun());
            }
        }
        else
        {
            StopCoroutine(FireGun());
            isFiring = false;
        }
    }

    // Rotating the machine gun
    void RotateGun()
    {
        // Left (Num 4) and Right (Num 6) rotation for gunBase
        if (Input.GetKey(KeyCode.Keypad4))
        {
            gunBase.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Keypad6))
        {
            gunBase.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        // Up (Num 8) and Down (Num 2) rotation for gunBarrel
        if (Input.GetKey(KeyCode.Keypad8))
        {
            gunBarrel.Rotate(Vector3.right * -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Keypad2))
        {
            gunBarrel.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }

    // Firing bullets at a controlled fire rate
    IEnumerator FireGun()
    {
        isFiring = true;

        while (isFiring)
        {
            if (Time.time >= nextFireTime)
            {
                FireBullet();
                nextFireTime = Time.time + 1f / fireRate;
                particleSystem1.Play();
            }
            yield return null;
        }
    }

    // Instantiate and fire a bullet
    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Ensure the bullet has a Rigidbody and apply force to it
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }

        // Destroy the bullet after a set time to prevent clutter
        Destroy(bullet, bulletLifeTime);
    }

    // Bullet hit detection and impact effect
    public void BulletImpact(GameObject bullet, Collision collision)
    {
        // Spawn hit effect at the point of collision
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, collision.contacts[0].point, Quaternion.identity);
        }

        // Destroy the bullet upon impact
        Destroy(bullet);
    }
}
