using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;

public class turret_Control : TurretRotationBehavior
{
	public float speed = 10f;
    public Wheel_Control_CS wheelScript;

    void Start()
    {
        
    }

    void Update()
    {  
        //turretRotation();
        if (wheelScript.ActivateTank)
        {
            networkObject.SendRpc(RPC_TURRET, Receivers.All, transform.rotation);

            if (Input.GetKey(KeyCode.R))
            {
                transform.Rotate(Vector3.up * speed * Time.deltaTime);
                //networkObject.SendRpc(RPC_TURRET, Receivers.All, transform.rotation);
            }

            if (Input.GetKey(KeyCode.T))
            {
                transform.Rotate(-Vector3.up * speed * Time.deltaTime);
                //networkObject.SendRpc(RPC_TURRET, Receivers.All, transform.rotation);
            }
        }
    }

    public override void Turret(RpcArgs args)
    {
        // RPC calls are not made from the main thread for performance, since we
        // are interacting with Unity engine objects, we will need to make sure
        // to run the logic on the main thread

        if (!wheelScript.ActivateTank)
        {
            MainThreadManager.Run(() =>
            {
                transform.rotation = args.GetNext<Quaternion>();

            });
        }
    }
}
