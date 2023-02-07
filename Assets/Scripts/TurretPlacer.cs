using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacer : MonoBehaviour {

	public Transform turret;

	float pitchDeg;
	float yawDeg;
	
	public bool invertYAxis = false;

	void Awake() {
		Vector3 startEuler = transform.eulerAngles;
		pitchDeg = startEuler.x;
		yawDeg = startEuler.y;
		transform.rotation = Quaternion.Euler( pitchDeg, yawDeg, 0 );
	}
	
	void Update()
	{
		Ray ray = new Ray( transform.position, transform.forward );
		if( Physics.Raycast( ray, out RaycastHit hit ) ) {
			turret.position = hit.point;
			Vector3 yAxis = hit.normal;
			Vector3 zAxis = Vector3.Cross( transform.right, yAxis ).normalized;
			// Debug.color = new Color( 1, 1, 1, 0.4f );
			Debug.DrawLine( ray.origin, hit.point,new Color( 1, 1, 1, 0.4f ) );
			turret.rotation = Quaternion.LookRotation( zAxis, yAxis );
		}
		UpdateMouseLook();
	}

	void UpdateMouseLook() {
		float xDelta = Input.GetAxis( "Mouse X" );
		float yDelta = Input.GetAxis( "Mouse Y" );
		Debug.Log( $"xDelta={xDelta}, yDelta={yDelta}" );
		pitchDeg += invertYAxis ? yDelta : -yDelta;
		pitchDeg = Mathf.Clamp( pitchDeg, -90, 90 );
		yawDeg += xDelta;
		transform.rotation = Quaternion.Euler( pitchDeg, yawDeg, 0 );
	}

}