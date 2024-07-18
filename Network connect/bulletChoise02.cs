﻿using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletChoise02 : SpawnBulletBehavior
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

        particleSystem1.Stop();
        particleSystem2.Stop();
    }
    void Update()
    {
        if (activeTank == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                networkObject.SendRpc(RPC_LOAD_HEAT_AMMO, Receivers.All);
            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                networkObject.SendRpc(RPC_LOAD_HE_AMMO, Receivers.All);
            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                networkObject.SendRpc(RPC_LOAD, Receivers.All);

            }


            if (Input.GetKeyDown(KeyCode.F))
            {
                networkObject.SendRpc(RPC_MAIN_GUN_FIRE, Receivers.All);
            }
        }
    }

    public override void mainGunFire(RpcArgs args)
    {
        velocitymps = velocitykmph * 1000f / 3600f;
        if (loaded && Time.time - lastFireTime >= fireCooldown)
        {
            if (bulletHe)
            {

                GameObject bullet = Instantiate(spawnItem1, spawnPoint.position, spawnPoint.rotation);
                rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(spawnPoint.forward * velocitymps, ForceMode.VelocityChange);
                loaded = false;
                particleSystem1.Play();
                particleSystem2.Play();
            }
            else if (bulletHeat)
            {

                GameObject bullet = Instantiate(spawnItem2, spawnPoint.position, spawnPoint.rotation);
                rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(spawnPoint.forward * velocitymps * 2, ForceMode.VelocityChange);
                loaded = false;
                particleSystem1.Play();
                particleSystem2.Play();
            }

            lastFireTime = Time.time;
        }
    }

    public override void Load(RpcArgs args)
    {
        loaded = true;
    }

    public override void LoadHeAmmo(RpcArgs args)
    {
        bulletHe = true;
        bulletHeat = false;
    }

    public override void LoadHeatAmmo(RpcArgs args)
    {
        bulletHe = false;
        bulletHeat = true;
    }
}
