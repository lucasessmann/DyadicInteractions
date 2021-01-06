using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mirror.Examples.Benchmark
{
	public class PlayerBehaviour : NetworkBehaviour
	{
		
		public LineRenderer laserLineRenderer;
		public GameObject gazeSphere;
		public GameObject cameraRig;
		public float laserWidth = 0.02f;
		public float laserMaxLength = 10f;
		 
		// Start is called before the first frame update
		void Start()
		{
			Vector3[] initLaserPositions = new Vector3[ 2 ] { Vector3.zero, Vector3.zero };
			laserLineRenderer.SetPositions( initLaserPositions );
			//laserLineRenderer.SetWidth( laserWidth, laserWidth );

			if (isLocalPlayer) {
				gazeSphere.GetComponent<Renderer>().material.color = new Color32(0,255,255,66);
			}

		}

		// Update is called once per frame
		void Update()
		{
			ShootLaserFromTargetPosition( cameraRig.transform.position, cameraRig.transform.rotation * Vector3.forward, laserMaxLength );
			laserLineRenderer.enabled = true;
		}
		
		void LateUpdate ()
		{
			cameraRig.transform.position = this.transform.position;
		}
		
		void ShootLaserFromTargetPosition( Vector3 targetPosition, Vector3 direction, float length )
		{
			Ray ray = new Ray( targetPosition, direction );
			RaycastHit raycastHit;
			Vector3 endPosition = targetPosition + ( length * direction );
	 
			if( Physics.Raycast( ray, out raycastHit, length ) ) {
				endPosition = raycastHit.point;
			}
	 
			laserLineRenderer.SetPosition( 0, targetPosition );
			laserLineRenderer.SetPosition( 1, endPosition );
			
			gazeSphere.transform.position = endPosition;
			
		}
	 
	}
}