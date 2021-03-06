﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform backGround;
	
	private Transform playerTransform;
	private const float DISTANCE_FROM_PLAYER = -2F;
	private const float CAMERA_LOWER_LIMIT = 8F;
	
	
	public static CameraController Instance;
    
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;   
		}
		else
		{
			Destroy(this);
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		playerTransform = PlayerController.Instance.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateBackgroundPosition();
		
		if (playerTransform.position.y + DISTANCE_FROM_PLAYER > transform.position.y)
		{
			Vector3 cameraPosition = transform.position;
			cameraPosition.y = playerTransform.position.y + DISTANCE_FROM_PLAYER;
			transform.position = cameraPosition;
		}

		if (playerTransform.position.y < transform.position.y - CAMERA_LOWER_LIMIT)
		{
			PlayerController.Instance.LeavingCameraViewPort();
			Color c = Color.HSVToRGB(1f,1,1);
		}
	}

	private void UpdateBackgroundPosition()
	{
		if (backGround.localPosition.y < -22F)
		{
			return;
		}
		backGround.localPosition = new Vector3(
			backGround.localPosition.x,
			25f - Mathf.Sqrt(transform.position.y)*3f,
			backGround.localPosition.z);
	}

	public void SmoothMoveToY(float yPosition)
	{

		StartCoroutine(SmoothMove(yPosition));
		
	}

	private IEnumerator SmoothMove(float yPosition)
	{
		float t = 0;
		while (t < 0.7f)
		{
			t += Time.deltaTime;
			transform.position = new Vector3(
				transform.position.x, 
				transform.position.y * (0.5f+((1f-t)*0.4f)) + yPosition * (0.1f+t*0.4f),
				transform.position.z
				);
			yield return null;
		}
	}
}
