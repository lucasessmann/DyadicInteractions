//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Controls for the non-VR debug camera
//
//=============================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Camera ) )]
	public class FallbackCameraController : MonoBehaviour
	{
		// added by Jasmin

		public Camera cam;
		public GameObject gazeSphere;
		
		// until here 
		public float speed = 4.0f;
		public float shiftSpeed = 16.0f;
		public bool showInstructions = true;
		public float RotateSpeed = 80f;

		private Vector3 startEulerAngles;
		private Vector3 startMousePosition;
		private float realTime;

		//-------------------------------------------------
		void OnEnable()
		{
			realTime = Time.realtimeSinceStartup;
			
		}


		//-------------------------------------------------
		void Update()
		{
			// added by Jasmin

			Ray gazeRay = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(gazeRay, out hit, 100f))
			{
				gazeSphere.transform.position = hit.point;
			}
			
			// until here
			float forward = 0.0f;
			if ( Input.GetKey( KeyCode.W ) || Input.GetKey( KeyCode.UpArrow ) )
			{
				//forward += 1.0f;
				transform.Rotate(-Vector3.right * RotateSpeed * Time.deltaTime);
				
			}
			if ( Input.GetKey( KeyCode.S ) || Input.GetKey( KeyCode.DownArrow ) )
			{
				//forward -= 1.0f;
				transform.Rotate(Vector3.right * RotateSpeed * Time.deltaTime);
			}

            /*float up = 0.0f;
            if (Input.GetKey(KeyCode.E))
            {
                up += 1.0f;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                up -= 1.0f;
            }*/

            float right = 0.0f;
			if ( Input.GetKey( KeyCode.D ) || Input.GetKey( KeyCode.RightArrow ) )
			{
				//right += 1.0f;
				transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime,Space.World);
			}
			if ( Input.GetKey( KeyCode.A ) || Input.GetKey( KeyCode.LeftArrow ) )
			{
				//right -= 1.0f;
				transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime,Space.World);
			}

			/*
			float currentSpeed = speed;
			if ( Input.GetKey( KeyCode.LeftShift ) || Input.GetKey( KeyCode.RightShift ) )
			{
				currentSpeed = shiftSpeed;
			}
			*/

			float realTimeNow = Time.realtimeSinceStartup;
			float deltaRealTime = realTimeNow - realTime;
			realTime = realTimeNow;

			// Vector3 delta = new Vector3( right, up, forward ) * currentSpeed * deltaRealTime;
			//
			// transform.position += transform.TransformDirection( delta );

			Vector3 mousePosition = Input.mousePosition;

			if ( Input.GetMouseButtonDown( 1 ) /* right mouse */)
			{
				startMousePosition = mousePosition;
				startEulerAngles = transform.localEulerAngles;
			}

			if ( Input.GetMouseButton( 1 ) /* right mouse */)
			{
				Vector3 offset = mousePosition - startMousePosition;
				transform.localEulerAngles = startEulerAngles + new Vector3( -offset.y * 360.0f / Screen.height, offset.x * 360.0f / Screen.width, 0.0f );
			}
		}


		//-------------------------------------------------
		void OnGUI()
		{
			if ( showInstructions )
			{
				GUI.Label(new Rect(10.0f, 10.0f, 600.0f, 400.0f),
					"WASD or Arrow Keys to rotate the camera\n" +
					"Fast rotation with right mouse click\n");
			}
		}
	}
}
