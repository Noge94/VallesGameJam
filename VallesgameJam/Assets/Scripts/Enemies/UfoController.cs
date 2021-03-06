﻿using System.Collections;
using UnityEngine;

public class UfoController : MonoBehaviour
{
	private bool canRotate;

	private const float MAX_ROTATION = 30f;

	private int healthPoints = 3;
	
	protected virtual void Update()
	{
		if (transform.position.y < CameraController.Instance.transform.position.y - 10f)
		{
			Destroy(gameObject);
			return;
		}
		
		if (!canRotate)
		{
			return;
		}
		
		UpdateRotation();
	}

	private void UpdateRotation()
	{
		float rotationn = transform.rotation.eulerAngles.z;
		if (rotationn > 90f) rotationn -= 360f;

		rotationn *= 0.95f;

		if (rotationn > MAX_ROTATION)
		{
			rotationn = MAX_ROTATION;
		}

		if (rotationn < -MAX_ROTATION)
		{
			rotationn = -MAX_ROTATION;
		}

		rotationn -= Input.GetAxis("Horizontal") * 1.2f;

		transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationn));
	}

	public void Hit()
	{
		healthPoints--;

		if (healthPoints < 1)
		{
			StartCoroutine(Explode());
		}
		else
		{
			StartCoroutine(HitAnimation());
		}
	}

	IEnumerator HitAnimation()
	{
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return null;
		yield return null;
		yield return null;
		GetComponent<SpriteRenderer>().color = Color.white;
	}


	public IEnumerator Explode()
	{
		GetComponent<Animator>().SetTrigger("Death");
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BPM>().setPlayExplosionTrue();
		yield return new WaitForSeconds(0.1f);
		ScoreDisplayer.Instance.AddScore(50);
        ScoreDisplayer.Instance.AddDinner(1);
		PlayerController.Instance.Jump();
		yield return new WaitForSeconds(0.3f);
		//Debug.Log("<color=red>BOOM, current up vector is: </color>"+transform.up);
		Destroy(this.gameObject);
	}

	public void UnderPlayer()
	{
		canRotate = true;
		StartCoroutine(AttackPlayer());
	}

	private IEnumerator AttackPlayer()
	{
		yield return new WaitForSeconds(2.5f);
		PlayerController.Instance.Hit();
		StartCoroutine(AttackPlayer());
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}
}
