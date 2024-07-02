using UnityEngine;
using System.Collections;

// This script must be attached to all the Driving Wheels.

public class Wheel_Rotate_CS : MonoBehaviour {

	public bool isLeft ;
	Rigidbody thisRigidbody ;
	public float maxAngVelocity ;
	Wheel_Control_CS controlScript ;

	Transform thisTransform ;
	Vector3 initialAng ;
    float radius;

    public float BrakingInitialSpeed;
    public float DecVelocity;


    void Start () {
		this.gameObject.layer = 9 ; // Layer9 >> for wheels.
		thisRigidbody = GetComponent < Rigidbody > () ;
		// Set direction.
		if ( transform.localPosition.y > 0.0f ) {
			isLeft = true ;
		} else {
			isLeft = false ;
		}
		// Get initial rotation.
		thisTransform = transform ;
		initialAng = thisTransform.localEulerAngles ;
	}

	void Get_Wheel_Control ( Wheel_Control_CS tempScript ) {
		controlScript = tempScript ;
		radius = GetComponent < SphereCollider > ().radius ;
		//maxAngVelocity = Mathf.Deg2Rad * ( ( controlScript.maxSpeed / ( 2.0f * Mathf.PI * radius ) ) * 360.0f ) ;
	}
	
	void FixedUpdate () {
        maxAngVelocity = Mathf.Deg2Rad * ((controlScript.maxSpeed / (2.0f * Mathf.PI * radius)) * 360.0f);
        float rate ;
		if ( isLeft ) {
			rate = controlScript.leftRate ;
		} else {
			rate = controlScript.rightRate ;
		}
        //thisRigidbody.

        if (controlScript.HandBrake)
        {
            controlScript.thisRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
        }

        else if (controlScript.Braking)
        {
            if (Mathf.Round(DecVelocity) > 0)
            {
                //thisRigidbody.maxAngularVelocity -= 0.5f;
                DecVelocity = DecVelocity - 0.5f;
                thisRigidbody.maxAngularVelocity = Mathf.Abs(DecVelocity * 2 * rate);
                thisRigidbody.AddRelativeTorque(0.0f, (1 - rate) * 500, 0.0f);
            }
            else
            {
                controlScript.thisRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
            }
            
        }
        else
        {
            BrakingInitialSpeed = thisRigidbody.maxAngularVelocity;
            controlScript.thisRigidbody.constraints = RigidbodyConstraints.None;

            if (controlScript.Decelerating && DecVelocity > 0)
            {
                DecVelocity = DecVelocity - 0.1f;
                //thisRigidbody.maxAngularVelocity = Mathf.Abs(thisRigidbody.maxAngularVelocity - 0.1f);
                //thisRigidbody.maxAngularVelocity = Mathf.Abs((DecVelocity * rate) - 0.05f);
                thisRigidbody.maxAngularVelocity = Mathf.Abs(DecVelocity * 2 * rate);
                thisRigidbody.AddRelativeTorque(0.0f, (rate) * 1000, 0.0f);
            }
            else
            {
                DecVelocity = Mathf.Deg2Rad * ((controlScript.CurrentSpeed / (2.0f * Mathf.PI * radius)) * 360.0f);
                thisRigidbody.maxAngularVelocity = Mathf.Abs(maxAngVelocity * rate);
                thisRigidbody.AddRelativeTorque(0.0f, (rate) * controlScript.wheelTorque, 0.0f);//(rate) * controlScript.wheelTorque, 0.0f); 
            }
        }
        
        //Debug.Log(thisRigidbody.maxAngularVelocity);
        //thisRigidbody.maxAngularVelocity = maxAngVelocity;
        /*if (Input.GetKey(KeyCode.B))
        {
            //thisRigidbody.angularVelocity = Vector3.zero;
            thisRigidbody.angularDrag = 20000.0f;
        }
        else
            thisRigidbody.angularDrag = 0.05f;*/
    }

	void Update () { // Stabilize wheels.
		float angY = thisTransform.localEulerAngles.y ;
		thisTransform.localEulerAngles = new Vector3 ( initialAng.x , angY , initialAng.z ) ;
	}
}
