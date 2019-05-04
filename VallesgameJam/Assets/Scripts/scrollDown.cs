using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = this.transform.position - new Vector3(0, .3f, 0);
	}
}
