using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.UI;

public class Fire_Controller : FireBehavior
//public class Fire_Controller : MonoBehaviour
{
    [Header("Fire control settings")]
    [Tooltip("Loading time. (Sec)")] public float reloadTime = 0.2f;
    [Tooltip("Recoil force with firing.")] public float recoilForce = 1000.0f;

    public bool isReady = true;

    Transform thisTransform;
   // Rigidbody parentRigidbody;

    public bool bullet_HE = false;
    //public bool bullet_AP = false;
    public bool bullet_HEAT = false;

    public bool fire100mmBool;
    //   private SerialInput Serial_Read_script;
    public Wheel_Control_CS wheelScript;
    public Text Tank_1;

    void Start()
    {
       // parentRigidbody = GetComponentInParent<Rigidbody>();
       // Serial_Read_script = transform.root.gameObject.GetComponent<SerialInput>();
    }

    void Update()
    {
        if (wheelScript.ActivateTank)
        {
            networkObject.SendRpc(RPC_FIRE_MAIN_GUN, Receivers.All, fire100mmBool);
            if (Input.GetKey(KeyCode.G) && isReady)
            {
                bullet_HE = true;
                bullet_HEAT = false;
                fire100mmBool = true;
                //networkObject.SendRpc(RPC_TURRET, Receivers.All, transform.rotation);
                
                Tank_1.text = fire100mmBool.ToString();

                if (fire100mmBool)
                {
                    BroadcastMessage("Fire", SendMessageOptions.DontRequireReceiver);
                }

            }
            
            /* else if (Input.GetKey(KeyCode.H) && isReady)
             {
                 bullet_HE = false;
                 bullet_AP = true;
                 bullet_HEAT = false;
                 BroadcastMessage("Fire", SendMessageOptions.DontRequireReceiver);
             }*/

            /* else if (Input.GetKey(KeyCode.J) && isReady)
             {
                 bullet_HE = false;
                 bullet_HEAT = true;
                 BroadcastMessage("Fire", SendMessageOptions.DontRequireReceiver);
             }*/
        }
    }


    void Fire()
    {
       // parentRigidbody.AddForceAtPosition(-transform.forward * recoilForce, transform.position, ForceMode.Impulse);
        isReady = false;
        StartCoroutine("Reload");

    }



    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        isReady = true;
        fire100mmBool = false;
    }

    public override void FireMainGun(RpcArgs args)
    {
        if (!wheelScript.ActivateTank)
        {
            MainThreadManager.Run(() =>
            {
                fire100mmBool = args.GetNext<bool>();

                if (fire100mmBool)
                {
                    BroadcastMessage("Fire", SendMessageOptions.DontRequireReceiver);
                }
                Tank_1.text = fire100mmBool.ToString();
            });
            // throw new System.NotImplementedException();
        }
    }

    public override void FirePositions(RpcArgs args)
    {
        // throw new System.NotImplementedException();
    }
}

