using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	[SerializeField]
	private Text _score;
	[SerializeField]
	private Text _bestScore;

	void Start()
	{
		_score.text = "Score: "+PlayerPrefs.GetInt("LastScore");
		_bestScore.text = "Best Score: "+PlayerPrefs.GetInt("BestScore");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump"))
		{
			SceneManager.LoadScene("Main");
		}
	}
}
