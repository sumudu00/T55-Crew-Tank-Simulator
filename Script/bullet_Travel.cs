using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.UI;

public class bullet_Travel : EndPointsBehavior
{
    bool isLive = true;
	bool isRayHit = false;
	
	Vector3 nextPos;
	public GameObject brokenObject;
	public GameObject brokenObject1;
	Rigidbody thisRigidbody;
	Fire_Controller fireCOntrol;

	public Wheel_Control_CS wheelScript;

	void Start()
    {
        thisRigidbody = GetComponent <Rigidbody>();
		fireCOntrol = GameObject.Find("MainGun").GetComponent<Fire_Controller>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
	{
		if (wheelScript.ActivateTank)
		{
			if (isLive)
				transform.LookAt(transform.position + thisRigidbody.velocity);
			if (isRayHit == false)
			{
				Ray ray = new Ray(transform.position, thisRigidbody.velocity);
				int layerMask = ~(1 << 10);
				RaycastHit raycastHit;
				Physics.Raycast(ray, out raycastHit, thisRigidbody.velocity.magnitude * Time.fixedDeltaTime, layerMask);

				if (raycastHit.collider)
				{

					nextPos = raycastHit.point;
					isRayHit = true;
				}

			}
			else
			{
				transform.position = nextPos;
				thisRigidbody.position = nextPos;
				isLive = false;
				Hit();
			}
			networkObject.SendRpc(RPC_BLAST, Receivers.All, nextPos);
			//networkObject.SendRpc(RPC_FIRE_POSITIONS, Receivers.All, muzzleFirePoint, bullet_HE_point);
		}
	
	}
	
	void OnColliderEnter (Collision collision)
	{
		
		if(isLive)
		{
			isLive = false;
			Hit();
		}
	}
	
	void Hit ()
	{
		
		Instantiate(brokenObject, transform.position , Quaternion.identity);
		
		
		/*if(fireCOntrol.bullet_HEAT)
		{
			Instantiate(brokenObject1, transform.position , Quaternion.identity);
		}*/
	}

    public override void blast(RpcArgs args)
    {
		// throw new System.NotImplementedException();
		if (!wheelScript.ActivateTank)
		{
			MainThreadManager.Run(() =>
			{
				Vector3 hitPoint = args.GetNext<Vector3>();

				nextPos = hitPoint;

			});
		}
	}
}
