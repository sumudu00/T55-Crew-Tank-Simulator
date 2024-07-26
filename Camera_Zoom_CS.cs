using UnityEngine;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;

// This script must be attached to "Main Camera".

// (Note.) Main Camera must be placed on X Aixs of "Camera_Pivot".

public class Camera_Zoom_CS : MonoBehaviour {

	Transform thisTransform ;
	Camera thisCamera ;
	AudioListener thisAudioListener ;

	float posX ;

	int myID ;
	bool isCurrentID ;

	void Awake () {
		thisCamera = GetComponent < Camera > () ;
		thisCamera.enabled = false ;
		thisAudioListener = GetComponent < AudioListener > () ;
		thisAudioListener.enabled = false ;
	}

	void Start () {
		thisTransform = transform ;
		posX = transform.localPosition.x ;
	}
	
	void Update () {
		if ( isCurrentID ) {
			#if UNITY_ANDROID || UNITY_IPHONE
			float axis = CrossPlatformInputManager.GetAxis ( "Zoom" ) ;
			if ( axis != 0 ) {
				posX -= axis * 0.5f ;
				posX = Mathf.Clamp ( posX , 5.0f , 30.0f ) ;
				thisTransform.localPosition = new Vector3 ( posX , thisTransform.localPosition.y , thisTransform.localPosition.z ) ;
			}
			#else
			float axis = Input.GetAxis ( "Mouse ScrollWheel" ) ;
			if ( axis != 0 ) {
				posX -= axis * 10.0f ;
				posX = Mathf.Clamp ( posX , 5.0f , 30.0f ) ;
				thisTransform.localPosition = new Vector3 ( posX , thisTransform.localPosition.y , thisTransform.localPosition.z ) ;
			}
			#endif
		}
	}

	void Get_ID ( int idNum ) {
		myID = idNum ;
	}
	
	void Get_Current_ID ( int idNum ) {
		if ( idNum == myID ) {
			isCurrentID = true ;
			thisCamera.enabled = true ;
			thisAudioListener.enabled = true ;
		} else {
			isCurrentID = false ;
			thisCamera.enabled = false ;
			thisAudioListener.enabled = false ;
		}
	}
}
