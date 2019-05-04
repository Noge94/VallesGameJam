using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

	[SerializeField] private GameObject ufoPrefab;
	[SerializeField] private GameObject movingUfoPrefab;
	
	[SerializeField] private GameObject bubblePrefab;

	private float nextUfoSpawnY = 0f;
	private float nextBubbleSpawnY = 30f;

	private Transform playerTransform;
	
	const float SEPARATION_BETWEEN_OVNIES = 3f;
	private float separationBetweenBubbles = 20f;
	
	void Start ()
	{
		playerTransform = PlayerController.Instance.transform;
		UpdateUfos();
	}

	private void Update()
	{
		UpdateUfos();
		UpdateBubbles();
	}

	private void UpdateBubbles()
	{
		if (playerTransform.position.y + 10f < nextBubbleSpawnY)
		{
			return;
		}

		SpawnGameObject(bubblePrefab, nextBubbleSpawnY);
		nextBubbleSpawnY += separationBetweenBubbles;
	}

	private void UpdateUfos()
	{
		if (playerTransform.position.y + 10f < nextUfoSpawnY)
		{
			return;
		}

		ScoreDisplayer.Instance.AddScore(10);
		SpawnGameObject(RandomUfo(), nextUfoSpawnY);
		nextUfoSpawnY += SEPARATION_BETWEEN_OVNIES;
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
