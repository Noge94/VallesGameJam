using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAttach : MonoBehaviour {

    public bool bubbleTouched = false;
    [SerializeField] 
    public GameObject playerBubble;

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

            Destroy(collision.transform.parent.gameObject);
            this.playerBubble.gameObject.SetActive(true);

        }
    }
}
