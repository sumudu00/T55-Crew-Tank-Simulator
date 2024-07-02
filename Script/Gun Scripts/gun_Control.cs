using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;

public class gun_Control : GunRotationBehavior
{
    public float speed = 10f;
	public float currentAng;

    public Wheel_Control_CS wheelScript;

    void Start()
    {
        
    }

    
    void Update()
    {
		currentAng = transform.localEulerAngles.x;
        gunRotation();
    }

	void gunRotation()
    {
		if (wheelScript.ActivateTank)
		{

			if (Input.GetKey(KeyCode.Y) && (currentAng > 341.0f || currentAng < 5.0f))
			{
				transform.Rotate(Vector3.right * speed * Time.deltaTime);
                networkObject.SendRpc(RPC_GUN, Receivers.All, transform.rotation);
            }

			if (Input.GetKey(KeyCode.U) && (currentAng > 342.0f || currentAng < 6.0f))
			{
				transform.Rotate(-Vector3.right * speed * Time.deltaTime);
                networkObject.SendRpc(RPC_GUN, Receivers.All, transform.rotation);
            }
		}
	}

    public override void Gun(RpcArgs args)
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
