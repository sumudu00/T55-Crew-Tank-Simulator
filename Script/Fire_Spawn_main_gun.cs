using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.UI;

public class Fire_Spawn_main_gun : FireBehavior
{
 
    [Header("Firing settings")]
    [Tooltip("Prefab of muzzle fire.")] public GameObject firePrefab;
    [Tooltip("Speed of the bullet. (Meter per Second)")] public float bulletVelocity = 1000.0f;
    [Tooltip("Offset distance for spawning the bullet. (Meter)")] public float spawnOffset = 0.2f;

    public GameObject bulletPrefab_HE;
    public GameObject bulletPrefab_HEAT;

    // bool bullet_HE = true;
    //bool bullet_AP = true;
    // bool bullet_HEAT = true;

    //private Fire_Control_100mm fireScript100mm;
    public Wheel_Control_CS wheelScript;
    public Fire_Controller FireScript;

    private Vector3 muzzleFirePoint;
    private Vector3 bullet_HE_point;

    public Text muzzlepointText;
    public Text bulletpointText;
    public Fire_Controller fireControlScript;

    void Start()
    {
        //fireScript100mm = GameObject.Find("Cannon_Base10mmD10T2S").GetComponent<Fire_Control_100mm>();
    }

    void Fire()
    {
        if (wheelScript.ActivateTank)
        {
            networkObject.SendRpc(RPC_FIRE_POSITIONS, Receivers.All, muzzleFirePoint, bullet_HE_point);

            if (FireScript.fire100mmBool)
            {
                if (firePrefab)
                {
                    muzzleFirePoint = transform.position - transform.up * spawnOffset;
                    GameObject fireObject = Instantiate(firePrefab, muzzleFirePoint, transform.rotation) as GameObject;
                    fireObject.transform.parent = transform;

                }

                if (fireControlScript.bullet_HE)
                {
                    if (bulletPrefab_HE)
                    {
                        bullet_HE_point = transform.position + transform.forward * spawnOffset;
                        GameObject bulletObject = Instantiate(bulletPrefab_HE, bullet_HE_point, transform.rotation) as GameObject;
                        bulletObject.GetComponent<Rigidbody>().velocity = transform.forward * bulletVelocity;
                    }
                }

                /* if (fireScript100mm.bullet_AP)
                 {
                     if (bulletPrefab_AP)
                     {
                         GameObject bulletObject = Instantiate(bulletPrefab_AP, transform.position + transform.forward * spawnOffset, transform.rotation) as GameObject;
                         bulletObject.GetComponent<Rigidbody>().velocity = transform.forward * bulletVelocity;
                         //Debug.LogError("bulletAP");
                     }
                 }*/

                if (fireControlScript.bullet_HEAT)
                {
                    if (bulletPrefab_HEAT)
                    {
                        GameObject bulletObject = Instantiate(bulletPrefab_HEAT, transform.position + transform.forward * spawnOffset, transform.rotation) as GameObject;
                        bulletObject.GetComponent<Rigidbody>().velocity = transform.forward * bulletVelocity;
                    }
                }
                //networkObject.SendRpc(RPC_FIRE_MAIN_GUN, Receivers.All, fire100mmBool);
               
                muzzlepointText.text = muzzleFirePoint.ToString();
                bulletpointText.text = muzzleFirePoint.ToString();

            }
        }
    }

    void Update()
    {

    }

    public override void FireMainGun(RpcArgs args)
    {
         //if (!wheelScript.ActivateTank)
         //{
         //    MainThreadManager.Run(() =>
         //    {
         //        FireScript.fire100mmBool = args.GetNext<bool>();
         //    });
         //}
        //throw new System.NotImplementedException();
    }

    public override void FirePositions(RpcArgs args)
    {
        //throw new System.NotImplementedException();
        if (!wheelScript.ActivateTank)
        {
            MainThreadManager.Run(() =>
            {
                //FireScript.fire100mmBool = args.GetNext<bool>();
                muzzleFirePoint = args.GetNext<Vector3>();
                bullet_HE_point = args.GetNext<Vector3>();

                muzzleFirePoint = transform.position - transform.up * spawnOffset;
                bullet_HE_point = transform.position + transform.forward * spawnOffset;
             
                muzzlepointText.text = muzzleFirePoint.ToString();
                bulletpointText.text = muzzleFirePoint.ToString();
            });
        }
    }
}