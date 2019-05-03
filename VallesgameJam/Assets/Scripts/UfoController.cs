using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoController : MonoBehaviour
{
	public bool canRotate;

	private const float MAX_ROTATION = 30f;
	
	private void Update()
	{
		if (!canRotate)
		{
			return;
		}

		
		
//		Vector3 rotation = transform.rotation.eulerAngles;
//		rotation.z -= Input.GetAxis("Horizontal");
//		if (rotation.z > 30f && rotation.z < 180f)
//		{
//			rotation.z = 30f;
//		}
//		else if (rotation.z < 330f && rotation.z > 180f)
//		{
//			rotation.z = 330f;
//		}
//		transform.rotation = Quaternion.Euler(rotation);

		
		
		float rotationn = transform.rotation.eulerAngles.z;
		if (rotationn > 180f) rotationn -= 360f;

		rotationn *= 0.95f;
		
		if (rotationn > MAX_ROTATION)
		{
			rotationn = MAX_ROTATION;
		}
		if (rotationn < -MAX_ROTATION)
		{
			rotationn = -MAX_ROTATION;
		}
		
		rotationn -= Input.GetAxis("Horizontal");
		
//		if (rotationn < 0) rotationn += 360f;
		
		transform.rotation = Quaternion.Euler(new Vector3(0,0,rotationn));



	}


	public void Explode()
	{
		Debug.Log("BOOM, current up vector is: "+transform.up);
	}
}
