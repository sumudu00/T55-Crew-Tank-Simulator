using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;


public class turret_spin : turret_spinBehavior
{
    //private TextMeshProUGUI textMeshPro;

    public float turretrotateSpeed = 40;
    public float eulerAngY = 0;
    public bool activeTank = false;

    private void Start()
    {
        //textMeshPro = GameObject.Find("turretangle").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {

        if (activeTank == true)
        {
            eulerAngY = transform.localEulerAngles.y;

            if (Input.GetKey(KeyCode.H))
            {
                //transform.Translate (-speed * Time.deltaTime,0,0);
                /*transform.Rotate(0, -turretrotateSpeed * Time.deltaTime, 0);*/
                //Rotate
                networkObject.SendRpc(RPC_MOVE_LEFT, Receivers.All);
            }

            if (Input.GetKey(KeyCode.K))
            {
                //transform.Translate (speed * Time.deltaTime,0,0);
                /*  transform.Rotate(0, turretrotateSpeed * Time.deltaTime, 0);*/                                 //Rotate
                networkObject.SendRpc(RPC_MOVE_RIGHT, Receivers.All);
            }

            if (Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.LeftShift))
            {
                networkObject.SendRpc(RPC_MOVE_LEFT_FAST, Receivers.All);
            }
            if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.LeftShift))
            {
                networkObject.SendRpc(RPC_MOVE_RIGHT_FAST, Receivers.All);
            }
        }
    }
            //if (Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.LeftShift))
            //{
            //    //transform.Translate (-speed * Time.deltaTime,0,0);
            //    transform.Rotate(0, -turretrotateSpeed * 8 * Time.deltaTime, 0);                                //Rotate faster
            //}

            //if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.LeftShift))
            //{
            //    //transform.Translate (speed * Time.deltaTime,0,0);
            //    transform.Rotate(0, turretrotateSpeed * 8 * Time.deltaTime, 0);                                 //Rotate faster
            //}
        
            //textMeshPro.text = "Turret Angle : " + eulerAngY.ToString("0.00") + "\u00B0";
    

    public override void moveLeft(RpcArgs args)
    {
        transform.Rotate(0, -turretrotateSpeed * Time.deltaTime, 0);
    }

    public override void moveRight(RpcArgs args)
    {
        transform.Rotate(0, turretrotateSpeed * Time.deltaTime, 0);
    }

    public override void moveLeftFast(RpcArgs args)
    {
        transform.Rotate(0, -turretrotateSpeed * 8 * Time.deltaTime, 0);
    }

    public override void moveRightFast(RpcArgs args)
    {
        transform.Rotate(0, turretrotateSpeed * 8 * Time.deltaTime, 0);
    }
}
