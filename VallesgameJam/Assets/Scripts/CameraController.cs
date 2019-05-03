using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


	[SerializeField] private Transform player;

	private const float DISTANCE_FROM_PLAYER = -2F;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (player.position.y + DISTANCE_FROM_PLAYER > transform.position.y)
		{
			Vector3 cameraPosition = transform.position;
			cameraPosition.y = player.position.y + DISTANCE_FROM_PLAYER;
			transform.position = cameraPosition;
		}
		


	}
}
