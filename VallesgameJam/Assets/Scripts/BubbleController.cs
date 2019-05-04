using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour {

    public bool shouldIMove = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(shouldIMove)
            this.transform.position = new Vector2(0, this.transform.position.y+0.02f);
	}
}
