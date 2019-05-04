using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour {

    public bool shouldIMove = true;
    public float BaseScale = 0.5f;
    public float BaseOffsetPower = 5.0f;
    public float Velocity = 3.0f;

    private Vector3 mScale;
    private Vector3 mPos;
    private Transform mTransform;

    void Awake()
    {
        mTransform = GetComponent<Transform>();
        mScale = mTransform.localScale;
        Vector3 currentScale = mScale * BaseScale;
        mTransform.localScale = currentScale;
        mPos = mTransform.localPosition;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(shouldIMove)
            this.transform.position = new Vector2(0, this.transform.position.y-0.05f);

        Vector3 currentScale;
        currentScale = mScale * BaseScale;
        currentScale.x += Mathf.Sin(Time.time * Velocity) * BaseScale * 0.1f;
        currentScale.y += Mathf.Cos(Time.time * Velocity) * BaseScale * 0.1f;
        mTransform.localScale = currentScale;
        Vector3 currentPos;
        currentPos = mPos;
        currentPos.y += Mathf.Sin(Time.time * Velocity * 0.5f) * BaseOffsetPower * BaseScale * 0.1f;
        mTransform.localPosition = currentPos;
    }

    //void setCurrentPosition(Vector3 newPosition)
    //{
    //    GetComponent<Transform>().localPosition = newPosition;
    //    mPos = newPosition;
    //}
}

