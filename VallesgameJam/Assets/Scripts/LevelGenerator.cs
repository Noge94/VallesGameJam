using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

	[SerializeField] private GameObject ufoPrefab;
	[SerializeField] private GameObject movingUfoPrefab;
	
	[SerializeField] private GameObject bubblePrefab;

	private float nextUfoSpawnY = 0f;
	private float nextBubbleSpawnY = 15f;

	private Transform playerTransform;
	
	const float SEPARATION_BETWEEN_OVNIES = 3f;
	private float separationBetweenBubbles = 10f;
	
	void Start ()
	{
		playerTransform = PlayerController.Instance.transform;
		UpdateUfos();
		UpdateBubbles();
	}

	private void Update()
	{
		UpdateUfos();
		UpdateBubbles();
	}

	private void UpdateBubbles()
	{
		if (playerTransform.position.y + 20f < nextBubbleSpawnY)
		{
			return;
		}
		Instantiate(
			bubblePrefab, 
			new Vector3(
				Random.Range(-Configuration.SCREEN_LIMIT/2f, Configuration.SCREEN_LIMIT/2f),
				nextBubbleSpawnY,
				0),
			Quaternion.identity);
		
		nextBubbleSpawnY += separationBetweenBubbles;
		separationBetweenBubbles += 1f;
	}

	private void UpdateUfos()
	{
		if (playerTransform.position.y + 20f < nextUfoSpawnY)
		{
			return;
		}

		ScoreDisplayer.Instance.AddScore(10);
		
		switch (Random.Range(0,2))
		{
			case 0:
				SpawnMovingUfo();
				break;
			case 1:
				SpawnStaticUfo();
				break;
		}
		
		
//		SpawnGameObject(RandomUfo(), nextUfoSpawnY);
		nextUfoSpawnY += SEPARATION_BETWEEN_OVNIES;
	}

	private void SpawnStaticUfo()
	{
		Instantiate(
			ufoPrefab, 
			new Vector3(
				Random.Range(-Configuration.SCREEN_LIMIT, Configuration.SCREEN_LIMIT),
				nextUfoSpawnY,
				0),
			Quaternion.identity);
	}

	private void SpawnMovingUfo()
	{
		MovingUfoController ufo = Instantiate(
			movingUfoPrefab, 
			new Vector3(
				Random.Range(-Configuration.SCREEN_LIMIT, Configuration.SCREEN_LIMIT),
				nextUfoSpawnY,
				0),
			Quaternion.identity).GetComponent<MovingUfoController>();

		ufo.SetSpeed(1f+Random.Range(0, nextUfoSpawnY/20f));
	}

	private void SpawnGameObject(GameObject gobject, float positionY)
	{
		Instantiate(
			gobject, 
			new Vector3(
				Random.Range(-Configuration.SCREEN_LIMIT, Configuration.SCREEN_LIMIT),
				positionY,
				0),
			Quaternion.identity);
	}

	private GameObject RandomUfo()
	{
		switch (Random.Range(0,2))
		{
			case 0:
				return ufoPrefab;
			case 1:
				return movingUfoPrefab;
		}
		Debug.LogError("Random didnt work what");
		return ufoPrefab;
	}
	
	

}
