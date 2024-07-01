using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class moveTank : MonoBehaviour
{
    public List<WheelCollider> Front_Wheels; //The front wheels
    public List<WheelCollider> Back_Wheels; //The rear wheels 

    public List<Transform> Front_Wheel_Transforms; //The front wheel transforms
    public List<Transform> Back_Wheel_Transforms; //The rear wheel transforms

    public List<Vector3> Front_Wheel_Rotation; //The front wheel rotation Vectors
    public List<Vector3> Back_Wheel_Rotation; //The rear wheel rotation Vectors

    public float Motor_Torque = 400; //Motor torque for the car
    public float Max_Steer_Angle = 25f; //The Maximum Steer Angle for the front wheels
    public float BrakeForce = 150f; //The brake force of the wheels
    public float Maximum_Speed; //The top speed of the car

    public float handBrakeFrictionMultiplier = 2; //The handbrake friction multiplier
    private float handBrakeFriction = 0.05f; //The handbrake friction
    public float tempo; //Tempo (don't edit this)

    public bool Use_Car_States; //Use car states?
    public bool Car_Started; //Car stared?
    public KeyCode Car_Start_Key; //Key to start the car
    public KeyCode Car_Off_Key; //Key to turn car off

    public Transform Center_of_Mass; //Centre of mass of car
    public float frictionMultiplier = 3f; //Friction Multiplier
    public Rigidbody Car_Rigidbody; //Car rigidbody
    public float Car_Speed_KPH; //The car speed in KPH
    public float Car_Speed_MPH; //The car speed in MPH

    public int Car_Speed_In_KPH; //Car speed in KPH (integer form)
    public int Car_Speed_In_MPH; //Car speed in MPH (integer form)

    public bool Is_Flying() //bool for if the car is flying or not
    {
        if (!Back_Wheels[0].isGrounded && !Front_Wheels[0].isGrounded)
        {
            return true;
        }
        else
            return false;
    }

    private Rigidbody rb; //The rb
    private float Brakes = 0f; //Brakes


    [HideInInspector] public float currSpeed; //Current speed


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Center_of_Mass.localPosition;
    }
    public void FixedUpdate()
    {
        //Turning car off
        if (Input.GetKeyDown(Car_Off_Key) && (Car_Speed_KPH >= 0 && Car_Speed_KPH <= 1.5f) && Use_Car_States)
        { //if the car off key has been pressed and the car speed is 0 and the "use car states" is true
            Turn_Off_Car(); //Turn car off
        }

        //Turning Car on
        if (Input.GetKeyDown(Car_Start_Key) && Use_Car_States)
        { //if the "use car states" is true and that the car start key is pressed
            Car_Started = true;
        }

        //If the car states are not in use
        if (!Use_Car_States)
        {
            Car_Started = true;
        }

        //Applying Maximum Speed
        if (Car_Speed_In_KPH < Maximum_Speed && Car_Started)
        { //if the car's current speed is less than the maximum speed
            //Let car move forward and backward
            foreach (WheelCollider Wheel in Back_Wheels)
            {
                Wheel.motorTorque = Input.GetAxis("Vertical") * ((Motor_Torque * 5) / (Back_Wheels.Count + Front_Wheels.Count));
            }
        }

        if (Car_Speed_In_KPH > Maximum_Speed && Car_Started)
        { //if the car's current speed is more than the top speed
            //Don't let the car accelerate anymore so it does not exceed the maximum speed
            foreach (WheelCollider Wheel in Back_Wheels)
            {
                Wheel.motorTorque = 0;
            }
        }
        // apply torque for front whelll

        if (Car_Speed_In_KPH < Maximum_Speed && Car_Started)
        { //if the car's current speed is less than the maximum speed
            //Let car move forward and backward
            foreach (WheelCollider Wheel in Front_Wheels)
            {
                Wheel.motorTorque = Input.GetAxis("Vertical") * ((Motor_Torque * 5) / (Back_Wheels.Count + Front_Wheels.Count));
            }
        }

        if (Car_Speed_In_KPH > Maximum_Speed && Car_Started)
        { //if the car's current speed is more than the top speed
            //Don't let the car accelerate anymore so it does not exceed the maximum speed
            foreach (WheelCollider Wheel in Front_Wheels)
            {
                Wheel.motorTorque = 0;
            }
        }

        //Making The Car Turn/Steer
        //if (Car_Started)
        //{
        //    foreach (WheelCollider Wheel in Front_Wheels)
        //    {
        //        Wheel.steerAngle = Input.GetAxis("Horizontal") * Max_Steer_Angle; //Turn the wheels
        //    }
        //}
        //Changing speed of the car
        Car_Speed_KPH = Car_Rigidbody.velocity.magnitude * 3.6f; //Calculate car speed in KPH
        Car_Speed_MPH = Car_Rigidbody.velocity.magnitude * 2.237f; //Calculate the car's speed in MPH

        Car_Speed_In_KPH = (int)Car_Speed_KPH; //Convert the float values of the speed to int
        Car_Speed_In_MPH = (int)Car_Speed_MPH; //Convert the float values of the speed to int


    }

    public void Update()
    {

        //Rotating The Wheels Meshes so they have the same position and rotation as the wheel colliders
        var pos = Vector3.zero; //position value (temporary)
        var rot = Quaternion.identity; //rotation value (temporary)

        for (int i = 0; i < (Back_Wheels.Count); i++)
        {
            Back_Wheels[i].GetWorldPose(out pos, out rot); //get the world rotation & position of the wheel colliders
            Back_Wheel_Transforms[i].position = pos; //Set the wheel transform positions to the wheel collider positions
            Back_Wheel_Transforms[i].rotation = rot * Quaternion.Euler(Back_Wheel_Rotation[i]); //Rotate the wheel transforms to the rotation of the wheel collider(s) and the rotation offset
        }

        for (int i = 0; i < (Front_Wheels.Count); i++)
        {
            Front_Wheels[i].GetWorldPose(out pos, out rot); //get the world rotation & position of the wheel colliders
            Front_Wheel_Transforms[i].position = pos; //Set the wheel transform positions to the wheel collider positions
            Front_Wheel_Transforms[i].rotation = rot * Quaternion.Euler(Front_Wheel_Rotation[i]); //Rotate the wheel transforms to the rotation of the wheel collider(s) and the rotation offset
        }
        if (Input.GetKey(KeyCode.Space) && Car_Started)
        {
            Brakes = BrakeForce;

        }
        else
        {
            Brakes = 0f;
        }
        //Apply brake force
        foreach (WheelCollider Wheel in Front_Wheels)
        {
            Wheel.brakeTorque = Brakes; //set the brake torque of the wheels to the brake torque
        }

        foreach (WheelCollider Wheel in Back_Wheels)
        {
            Wheel.brakeTorque = Brakes; //set the brake torque of the wheels to the brake torque
        }

    }
    public void Turn_Off_Car()
    {
        Car_Started = false;
    }



}

