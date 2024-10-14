using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class machineGunConteoller : MonoBehaviour
{
    public Transform gunBase; // The base of the machine gun (for left/right rotation)
    public Transform gunBarrel; // The barrel of the machine gun (for up/down rotation)
    public GameObject bulletPrefab; // The bullet prefab
    public Transform firePoint; // Where the bullet will spawn
    public float rotationSpeed = 50f; // Speed of the gun rotation
    public float bulletForce = 30f; // Force applied to bullets
                                     //  public GameObject muzzleFlashPrefab; // Particle system for muzzle flash
    public float bulletLifeTime = 3f; // Bullet lifetime
    public float fireRate = 10f; // Bullets per second in burst mode
    public int maxBullets = 120; // Maximum ammo before reload
    public float reloadTime = 2f; // Reload time in seconds
    public float minElevation = -30f; // Minimum barrel elevation
    public float maxElevation = 30f; // Maximum barrel elevation

    private int bulletsFired = 0; // Number of bullets fired
    private bool isReloading = false;
    private bool isFiring = false;
    private float nextFireTime = 0f;
    private TextMeshProUGUI text;
    private ParticleSystem particleSystem1;
    //private ParticleSystem particleSystem2;

    // Fire modes
    [SerializeField] private bool singleShot = false;
    [SerializeField] private bool tripleShot = false;
    [SerializeField] private bool burstFire = false;

    private void Start()
    {
        text = GameObject.Find("bullet_Count").GetComponent<TextMeshProUGUI>();
        particleSystem1 = GameObject.Find("VFX_MuzzleFlash 1").GetComponent<ParticleSystem>();
       // particleSystem1 = GameObject.Find("fire_particle").GetComponent<ParticleSystem>();

        particleSystem1.Stop();
    }

    void Update()
    {
        // Gun movement controls
        RotateGun();

        // Fire mode selection
        if (Input.GetKeyDown(KeyCode.Keypad1)) { SetFireMode(true, false, false); }
        if (Input.GetKeyDown(KeyCode.Keypad3)) { SetFireMode(false, true, false); }
        if (Input.GetKeyDown(KeyCode.Keypad9)) { SetFireMode(false, false, true); }

        // Fire with the selected mode
        if (Input.GetKeyDown(KeyCode.Keypad5) && !isReloading)
        {
            if (singleShot && bulletsFired < maxBullets) { FireSingleShot(); }
            else if (tripleShot && bulletsFired + 3 <= maxBullets) { StartCoroutine(FireTripleShot()); }
            else if (burstFire && !isFiring) { StartCoroutine(FireBurst()); }
        }

        // Reload when pressing '7'
        if (Input.GetKeyDown(KeyCode.Keypad7) && bulletsFired >= maxBullets) { StartCoroutine(Reload()); }

        // Stop burst firing when the fire button is released
        if (Input.GetKeyUp(KeyCode.Keypad5) && burstFire) { isFiring = false; }
    }

    // Rotating the machine gun
    void RotateGun()
    {
        // Left and right rotation
        if (Input.GetKey(KeyCode.Keypad4)) { gunBase.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.Keypad6)) { gunBase.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); }

        // Up and down rotation with limitations
        float elevation = gunBarrel.localEulerAngles.x;
        elevation = (elevation > 180) ? elevation - 360 : elevation; // Normalize the angle

        if (Input.GetKey(KeyCode.Keypad8) && elevation > minElevation) { gunBarrel.Rotate(Vector3.right * -rotationSpeed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.Keypad2) && elevation < maxElevation) { gunBarrel.Rotate(Vector3.right * rotationSpeed * Time.deltaTime); }
    }

    // Set the fire mode
    void SetFireMode(bool single, bool triple, bool burst)
    {
        singleShot = single;
        tripleShot = triple;
        burstFire = burst;
    }

    // Single shot fire mode
    void FireSingleShot()
    {
        FireBullet();
    }

    // Triple shot fire mode (fires 3 bullets one at a time)
    IEnumerator FireTripleShot()
    {
        for (int i = 0; i < 3; i++)
        {
            FireBullet();
            yield return new WaitForSeconds(0.1f); // Delay between shots
        }
    }

    // Burst fire mode (continuous fire while holding the fire button)
    IEnumerator FireBurst()
    {
        isFiring = true;
        while (isFiring && bulletsFired < maxBullets)
        {
            if (Time.time >= nextFireTime)
            {
                FireBullet();
                nextFireTime = Time.time + 1f / fireRate; // Control fire rate
            }
            yield return null;
        }
    }

    // Instantiate and fire a bullet
    void FireBullet()
    {
        // Instantiate the bullet and apply force
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
        particleSystem1.Play();
        // Destroy the bullet after a set time to prevent clutter
        Destroy(bullet, bulletLifeTime);

        //// Play muzzle flash effect
        //if (muzzleFlashPrefab != null)
        //{
        //    Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        //}

        // Increment the bullet count
        bulletsFired++;
    }

    // Reload function
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime); // Wait for the reload to complete
        bulletsFired = 0; // Reset bullet count
        isReloading = false;
    }

}

