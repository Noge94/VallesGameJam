using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

	[SerializeField] private GameObject ufoPrefab;
	[SerializeField] private Transform playerTransform;

	public float nextSpawnY;
	
	const float SEPARATION_BETWEEN_OVNIES = 2f;
	const float MAX_X_POSITION = 5.5f;
	
	void Start ()
	{

		UpdateUfos();
		
	}

	private void Update()
	{
		UpdateUfos();
	}
	
	private void UpdateUfos()
	{
		if (playerTransform.position.y + 10f < nextSpawnY)
		{
			return;
		}

		SpawnUfo(nextSpawnY);
		nextSpawnY += SEPARATION_BETWEEN_OVNIES;
		
	}

	private void SpawnUfo(float positionY)
	{
		Instantiate(
			ufoPrefab, 
			new Vector3(
				Random.Range(-MAX_X_POSITION, MAX_X_POSITION),
				positionY,
				0),
			Quaternion.identity);
	}

}
