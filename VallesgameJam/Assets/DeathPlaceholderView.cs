using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPlaceholderView : MonoBehaviour
{


	[SerializeField] private Text ripText;
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(RIPAnimation());
	}

	private IEnumerator RIPAnimation()
	{
		ripText.text = "R";
		yield return new WaitForSeconds(0.4f);
		ripText.text = "RI";
		yield return new WaitForSeconds(0.4f);
		ripText.text = "RIP";
	}

}
