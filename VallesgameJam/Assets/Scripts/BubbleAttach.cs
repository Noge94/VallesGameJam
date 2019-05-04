using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAttach : MonoBehaviour {

    public bool bubbleTouched = false;
    public GameObject bubbleGO;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            Debug.Log("Bubble Touched!");
            bubbleTouched = true;

            bubbleGO = collision.transform.parent.gameObject;
            collision.transform.parent.gameObject.GetComponent<BubbleController>().shouldIMove = false;
            collision.GetComponent<Animator>().enabled = false;
            collision.gameObject.transform.parent.parent = this.transform;
            bubbleGO.transform.position = this.transform.position;

        }
    }
}
