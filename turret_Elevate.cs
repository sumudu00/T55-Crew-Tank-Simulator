using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
public class turret_Elevate : turret_ElevateBehavior
{
    //private TextMeshProUGUI textMeshPro;

    public float barrelelevateSpeed = 1.0f;
    public float eulerAngX = 0;
    public float rotationX;
    public bool activeTank = false;

    private void Start()
    {
        //textMeshPro = GameObject.Find("angle").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
       if (activeTank==true)
        {
            if (Input.GetKey(KeyCode.U))
            {
                networkObject.SendRpc(RPC_MOVE_UP, Receivers.All);
            }

            if (Input.GetKey(KeyCode.J))
            {
                networkObject.SendRpc(RPC_MOVE_DOWN, Receivers.All);
            }
        }

        //textMeshPro.text = "Barrel Angle : " + rotationX.ToString("0.00") + "\u00B0";
    }

    public override void moveUp(RpcArgs args)
    {
         transform.Rotate(-barrelelevateSpeed * Time.deltaTime, 0, 0);                        //Rotate //transform.Translate (-speed * Time.deltaTime,0,0);
         eulerAngX = transform.localEulerAngles.x;
        
        if (eulerAngX < 289 && eulerAngX > 286)                                                          //STOP Moving
        {
            transform.Rotate(+0.5f, 0, 0);
            eulerAngX = transform.localEulerAngles.x;
        }
    }

   

    public override void moveDown(RpcArgs args)
    {

       
        transform.Rotate(barrelelevateSpeed * Time.deltaTime, 0, 0);
       
        if (eulerAngX < 10 && eulerAngX > 5)                                                          //STOP Moving
        {
            transform.Rotate(-0.5f, 0, 0);
            eulerAngX = transform.eulerAngles.x;
        }

        if (eulerAngX <= 180f)
        {
            rotationX = -eulerAngX;
        }
        else
        {
            rotationX = -eulerAngX + 360f;
        }
    }

   
}
