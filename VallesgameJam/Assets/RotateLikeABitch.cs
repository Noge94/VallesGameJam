using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLikeABitch : MonoBehaviour
{
	private float lmao = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		lmao += 1f;
		transform.rotation = Quaternion.Euler( new Vector3(0, 0, lmao));
	}
}
