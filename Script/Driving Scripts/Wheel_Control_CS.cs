using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using BeardedManStudios.Forge.Networking.Generated;
using UnityStandardAssets.CrossPlatformInput;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;

// This script must be attached to "MainBody".

public class Wheel_Control_CS : MoveTankBehavior
{

	[ Header ( "Driving settings" ) ]
	[ Tooltip ( "Torque added to each wheel." ) ] public float wheelTorque = 2000.0f ; // Reference to "Wheel_Rotate".
	[ Tooltip ( "Maximum Speed (Meter per Second)" ) ] public float maxSpeed = 7.0f ; // Reference to "Wheel_Rotate".
	
    [Tooltip("Rate for ease of turning."), Range(0.0f, 1.0f)] public float turnClamp = 0.5f;
    [ Tooltip ( "Velocity the parking brake automatically works." ) ] public float autoParkingBrakeVelocity = 0.5f ;
	[ Tooltip ( "Lag time for activating the parking brake." ) ] public float autoParkingBrakeLag = 0.5f ;
	[ Tooltip ( "'Solver Iteration Count' of all the rigidbodies in this tank." ) ] public int solverIterationCount = 7 ;
    public float vertical;
    public float horizontal;


    public float leftRate ; // Reference to "Wheel_Rotate".
	public float rightRate ; // Reference to "Wheel_Rotate".

	public Rigidbody thisRigidbody ;

	public bool isParkingBrake = false ;
	public float lagCount ;

	int myID ;
	bool isCurrentID = true;


    [Header("Driving Key Inputs")]
    public KeyCode SystemONKey = KeyCode.Return;
    public KeyCode LeftSteerKey = KeyCode.Return;
    public KeyCode RightSteerKey = KeyCode.Return;
    public KeyCode ThrottleKey = KeyCode.Return;
    public KeyCode BrakeKey = KeyCode.Return;

    public float steerInputb = 0.0f;
    public float throttleInputb = 0.0f;
    public float brakeInputb = 0.0f;
    public float clutchInputb = 0.0f;
    private float prevClutchInput = 0.0f;

    public bool SystemIsON = false;
    public bool Serial = false;

    [Header("Driving Controllers")]
    public float[] GearMaxSpeed;
    public float[] GearMaxTorque;
    public int index = 0; //the gear no.
    //public float MaxSpeed; //Current gear max speed
    public float MaximumTorque;

    public bool Clutch = false;
    public bool Decelerating = false;
    public bool Braking = false;
    public bool HandBrake = false;
    public bool Steer = false;


    [Header("Driving Serial Inputs")]
    float PrevSerialIgnition = 0;
    public float SerialSystemON;
    public float ClutchInput;
    public string ClutchState;
    public bool CluthBalance = false;
    public float ThrottleInput;
    private float CurrentSteer;
    private float LeftSteerInput;
    private float RightSteerInput;
[Tooltip("Rate for ease of turning."), Range(0.0f, 1.0f)] public float turnClampL = 0.5f;
    [Tooltip("Rate for ease of turning."), Range(0.0f, 1.0f)] public float turnClampR = 0.5f;
    float BrakeInput;

    public float CurrentSpeed;

    private SerialScript SerialReadScript;

    private UIScript ButtonScript;

    public int TankNumber = 0;

    public Text StartKeyT;
    public Text GearT;
    public Text VerticalT;
    public Text HorizontalT;

    //public static bool ActivateTank = false;
    public bool ActivateTank = false;


    void Awake () {
		// Layer Collision Settings.
		// Layer9 >> for wheels.
		// Layer10 >> for Suspensions.
		// Layer11 >> for MainBody.
		for ( int i =0 ; i <= 11 ; i ++ ) {
			Physics.IgnoreLayerCollision ( 9 , i , false ) ; // Reset settings.
			Physics.IgnoreLayerCollision ( 11 , i , false ) ; // Reset settings.
		}
		Physics.IgnoreLayerCollision ( 9 , 9 , true ) ; // Wheels do not collide with each other.
		Physics.IgnoreLayerCollision ( 9 , 11 , true ) ; // Wheels do not collide with MainBody.
		for ( int i =0 ; i <= 11 ; i ++ ) {
			Physics.IgnoreLayerCollision ( 10 , i , true ) ; // Suspensions do not collide with anything.
		}
	}

	void Start () 
    {
		this.gameObject.layer = 11 ; // Layer11 >> for MainBody.
		thisRigidbody = GetComponent < Rigidbody > () ;
		thisRigidbody.solverIterations = solverIterationCount ;
		BroadcastMessage ( "Get_Wheel_Control" , this , SendMessageOptions.DontRequireReceiver ) ; // Send this reference to all the "Wheel_Rotate".
        SerialReadScript = GetComponent<SerialScript>();
        ButtonScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIScript>();

    }
	
	void FixedUpdate ()
    {
        
		if ( isCurrentID )
        {
#if UNITY_ANDROID || UNITY_IPHONE
			float vertical = CrossPlatformInputManager.GetAxis ( "Vertical" ) ;
			float horizontal = Mathf.Clamp ( CrossPlatformInputManager.GetAxis ( "Horizontal" ) , -turnClamp , turnClamp ) ;
#else
            //float vertical = Input.GetAxis ( "Vertical" ) ;
            //float horizontal = Mathf.Clamp ( Input.GetAxis ( "Horizontal" ) , -turnClamp , turnClamp ) ;

            if (ActivateTank) //CHANGED TO CLIENT CONTROL
            //if (networkObject.IsServer)
            {
                CurrentSpeed = thisRigidbody.velocity.magnitude;

                if (brakeInputb > 0)
                {
                    Braking = true;
                    vertical = brakeInputb;
                    Decelerating = false;
                }
                else
                {
                    Braking = false;

                    if ((throttleInputb == 0 || wheelTorque == 0) && Mathf.Round(CurrentSpeed) > 0)
                    {
                        Decelerating = true;
                        vertical = 0.5f;
                    }
                    else
                    {
                        Decelerating = false;
                        vertical = throttleInputb;

                    }
                }

                if (Mathf.Round(CurrentSpeed) > 0)
                    horizontal = Mathf.Clamp(steerInputb, -turnClamp, turnClamp);
                else
                    horizontal = 0;


#endif
                leftRate = Mathf.Clamp(-vertical - horizontal, -1.0f, 1.0f);
                rightRate = Mathf.Clamp(vertical - horizontal, -1.0f, 1.0f);

                ButtonScript.GearVal = index.ToString();
                ButtonScript.SpeedVal = Mathf.Round(CurrentSpeed).ToString();
            }

        }
        
    }

	void Update () {
        ////if (networkObject.IsServer) //CHANGED TO CLIENT CONTROL
        //////if (!networkObject.IsServer)
        ////{
        ////    transform.parent.transform.position = networkObject.Position3;
        ////    transform.parent.transform.rotation = networkObject.Rotation3;

        //    if (SerialReadScript.Serial == false)
        //    {
        //        //if (TankNumber == 1)
        //        //{
        //        //    transform.position = networkObject.Position1;
        //        //    transform.rotation = networkObject.Rotation1;
        //        //}
        //        //else if (TankNumber == 2)
        //        //{
        //        //    transform.position = networkObject.Position2;
        //        //    transform.rotation = networkObject.Rotation2;
        //        //}
        //        //else
        //        //{

        //        //}

        //    }
        ////}
        ////else
        ////{
        ///

        if (ActivateTank)
        {
            networkObject.SendRpc(RPC_MOVE, Receivers.All, transform.position, transform.rotation);


            StartKeyT.text = (SystemIsON) ? "ON" : "OFF";
            GearT.text = index.ToString();
            VerticalT.text = vertical.ToString();
            HorizontalT.text = horizontal.ToString();



            if (!SerialReadScript.Serial)
            {
                /*steerInputb = 0.0f;
                throttleInputb = 0.0f;
                brakeInputb = 0.0f;
                clutchInputb = 0.0f;*/

                Get_KeyInputs();

                /*if (brakeInputb == 1)
                {
                    isParkingBrake = true;
                    thisRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
                }*/


                if (SystemIsON)
                {
                    if (Input.GetKey(KeyCode.RightShift))
                        Clutch = true;
                    else
                        Clutch = false;

                    if (Clutch)
                    {
                        if (Input.GetKeyDown("2"))
                            index = 0;
                        if (Input.GetKeyDown("3"))
                            index = 1;
                        if (Input.GetKeyDown("4"))
                            index = 2;
                        if (Input.GetKeyDown("5"))
                            index = 3;
                        if (Input.GetKeyDown("6"))
                            index = 4;
                        if (Input.GetKeyDown("7"))
                            index = 5;
                        if (Input.GetKeyDown("8"))
                            index = 6;
                    }

                    maxSpeed = GearMaxSpeed[index];
                    wheelTorque = GearMaxTorque[index];


                }
            }

            //Serial Control
            else
            //if (SerialReadScript.Serial)
            {
                //Get the Clutch input
                ClutchInput = Mathf.Abs(SerialReadScript.ClutchInput * 10 / 1023);

                if (ClutchInput <= 7)
                {
                    if (ClutchInput > 5)
                        ClutchState = "Into Balance";
                    else if (ClutchInput <= 4 && ClutchInput > 0.2f)
                        ClutchState = "Out from Balance";
                    else if (ClutchInput <= 0.2f)
                        ClutchState = "Released";
                    else
                        ClutchState = "Balanced";
                }
                else
                {
                    ClutchState = "Fully Pressed";

                    index = (int)SerialReadScript.GearVal;
                    //maxSpeed = GearMaxSpeed[index];
                    MaximumTorque = GearMaxTorque[index];
                }

                //Get the steer input
                Set_Steer();

                //Get the brake input
                //BrakeInput = SerialReadScript.FootBrakeTorqueInput;
                if (SerialReadScript.FootBrakeTorqueInput < 400)
                {
                    brakeInputb = SerialReadScript.FootBrakeTorqueInput / 400;
                }
                else
                {
                    brakeInputb = 1;
                }


                //Get the throttle input

                if (SerialReadScript.MotorTorqueInput <= 600)
                {
                    ThrottleInput = SerialReadScript.MotorTorqueInput * 10 / 600;
                }
                else
                    ThrottleInput = 10;

                //Set the Ignition
                float CurrentIgnition = SerialReadScript.EngineState;

                if (CurrentIgnition == 0 && PrevSerialIgnition == 1)
                {
                    SerialSystemON = 1;
                }
                PrevSerialIgnition = CurrentIgnition;


                //Start the controlling
                if (SerialSystemON == 1)
                {

                    Set_MortorTorque();

                    Set_Throttle();


                }
                else
                {
                    SerialSystemON = 0;
                    wheelTorque = 0;
                }
            }


            if (!Braking || !Decelerating)
            {
                // Auto Parking Brake using 'RigidbodyConstraints'.
                if (leftRate == 0.0f && rightRate == 0.0f)
                {
                    AutoParkingBrake();


                }
                else
                {
                    isParkingBrake = false;
                    thisRigidbody.constraints = RigidbodyConstraints.None;
                    lagCount = 0.0f;
                }
            }
        }
        
        

            //if (TankNumber == 1)
            //{
            //    networkObject.Position1 = transform.position;
            //    networkObject.Rotation1 = transform.rotation;
            //}
            //else if (TankNumber == 2)
            //{
            //    networkObject.Position2 = transform.position;
            //    networkObject.Rotation2 = transform.rotation;
            //}
            //else
            //{
                //networkObject.Position3 = transform.parent.transform.position;
                //networkObject.Rotation3 = transform.parent.transform.rotation;
            //}
            
        //}

        

	}


    /// <summary>
    /// Used to move the cube that this script is attached to
    /// </summary>
    /// <param name="args">
    /// [0] Vector3 The direction/distance to move this cube by
    /// </param>
    public override void Move(RpcArgs args)
    {
        // RPC calls are not made from the main thread for performance, since we
        // are interacting with Unity engine objects, we will need to make sure
        // to run the logic on the main thread

        if (!ActivateTank)
        {
            MainThreadManager.Run(() =>
            {
                //transform.position += args.GetNext<Vector3>();
                transform.position = args.GetNext<Vector3>();
                transform.rotation = args.GetNext<Quaternion>();
            });
        }
            
        

    }

    void AutoParkingBrake()
    {
        float velocityMag = thisRigidbody.velocity.magnitude;
        float angularVelocityMag = thisRigidbody.angularVelocity.magnitude;
        if (isParkingBrake == false)
        {
            if (velocityMag < autoParkingBrakeVelocity && angularVelocityMag < autoParkingBrakeVelocity)
            {
                lagCount += Time.fixedDeltaTime;
                if (lagCount > autoParkingBrakeLag)
                {
                    isParkingBrake = true;
                    thisRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
                }
            }
        }
        else
        {
            if (velocityMag > autoParkingBrakeVelocity || angularVelocityMag > autoParkingBrakeVelocity)
            {
                isParkingBrake = false;
                thisRigidbody.constraints = RigidbodyConstraints.None;
                lagCount = 0.0f;
            }
        }
    }


    void Get_KeyInputs()
    {
        

        //Get the system ON input
        if (Input.GetKeyDown(SystemONKey))
        {
            if (SystemIsON)
                SystemIsON = false;
            else
                SystemIsON = true;
        }

        //Get the controlling inputs
        if (Input.GetKey(LeftSteerKey))
            steerInputb = -1.0f;
        else if (Input.GetKey(RightSteerKey))
            steerInputb = 1.0f;
        else
            steerInputb = 0;

        if (Input.GetKey(ThrottleKey))
        {
            if (index == 6)
                throttleInputb = -1.0f;
            else
                throttleInputb = 1.0f;
        }
        else
            throttleInputb = 0;

        if (Input.GetKey(BrakeKey))
            brakeInputb = 1.0f;
        else
            brakeInputb = 0;


    }
    
    void Set_Steer()
    {
        LeftSteerInput = SerialReadScript.LeftStick;
        RightSteerInput = SerialReadScript.RightStick;

        if (LeftSteerInput > 241 && LeftSteerInput < 261)
        {
            turnClampL = 0.2f;
        }
        else if (LeftSteerInput > 502 && LeftSteerInput < 522)
        {
            turnClampL = 0.5f;
        }
        else if (LeftSteerInput > 1008) //&& LeftSteerInput <= 1028)
        {
            turnClampL = 1.0f;
        }
        else
            turnClampL = 0;

        if (RightSteerInput > 241 && RightSteerInput < 261)
        {
            turnClampR = 0.2f;
        }
        else if (RightSteerInput > 502 && RightSteerInput < 522)
        {
            turnClampR = 0.5f;
        }
        else if (RightSteerInput > 1008) //&& RightSteerInput <= 1028)
        {
            turnClampR = 1.0f;
        }
        else
            turnClampR = 0;

        if (turnClampL == 0 && turnClampR > 0)
        {
            steerInputb = 1.0f;
            turnClamp = turnClampR;
        }
        else if (turnClampR == 0 && turnClampL > 0)
        {
            steerInputb = -1.0f;
            turnClamp = turnClampL;
        }
        else
        {
            steerInputb = 0;
            turnClamp = 0;
        }
    }

    void Set_MortorTorque()
    {

        if (CurrentSpeed == 0)
        {
            //wheelTorque = 0;
            //AutoParkingBrake();
            if (index != 0 && ClutchState == "Released")
            {
                wheelTorque = 0;
                SerialSystemON = 0;
                return;
            }
            else if (index == 1 || index == 6)
            {
                wheelTorque = MaximumTorque * Mathf.Clamp(((9 - ClutchInput) / 9), 0, 1);
            }
            else
            {
                wheelTorque = 0;
            }
        }
        else
        {

            if ((turnClampL == 0.2f && turnClampR == 0.2f))
            {
                wheelTorque = 0;
                HandBrake = false;
                //AutoParkingBrake();
            }
            else if ((turnClampL == 1.0f && turnClampR == 1.0f))
            {
                wheelTorque = 0;
                HandBrake = true;

            }
            else if ((turnClampL == 0.5f && turnClampR == 0.5f))
            {
                if (index == 1 || index == 0 || index == 6)
                {
                    wheelTorque = GearMaxTorque[index] * 1.5f;
                    maxSpeed = 2.0f;
                }
                else
                {
                    wheelTorque = GearMaxTorque[index - 1];
                    maxSpeed = GearMaxSpeed[index - 1];
                }
                HandBrake = false;
            } 
            else
            {
                wheelTorque = MaximumTorque * Mathf.Clamp(((9 - ClutchInput) / 9), 0, 1);
                maxSpeed = GearMaxSpeed[index];
                HandBrake = false;
            }
        }

    }

    void Set_Throttle()
    {
        if ((ClutchState == "Into Balance" || ClutchState == "Balanced") && (index == 1 || index == 6) && ThrottleInput < 1)
        {
            if (ClutchState == "Into Balance")
            {
                if (index == 1)
                    throttleInputb = (7.0f - ClutchInput) / 6;
                else if (index == 6)
                    throttleInputb = -((7.0f - ClutchInput) / 6);
            }
            else
            {
                if (index == 1)
                    throttleInputb = 0.33f;
                else if (index == 6)
                    throttleInputb = -(0.33f);
            }
        }
        else if (index != 6)
        {
            throttleInputb = ThrottleInput / 9;
        }
        else if (index == 6)
        {
            throttleInputb = ThrottleInput / 9;
        }
        else
        {
            throttleInputb = 0;
        }
    }


    void Get_ID ( int idNum ) {
		myID = idNum ;
	}

	void Get_Current_ID ( int idNum ) {
		if ( idNum == myID ) {
			isCurrentID = true ;
		} else {
			isCurrentID = false ;
		}
	}
}
