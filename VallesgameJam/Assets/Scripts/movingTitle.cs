using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movingTitle : MonoBehaviour {

    public Image mTitle;
    private float mTitleWithF;
    private float mTitleHeightF;
    private float mTitleWith;
    private float mTitleHeight;
    // Use this for initialization
    void Start () {
        mTitleWithF = mTitle.sprite.rect.width;
        mTitleHeightF = mTitle.sprite.rect.height;
	}
	
	// Update is called once per frame
	void Update () {
        mTitle.sprite.rect.Set(mTitle.sprite.rect.x, mTitle.sprite.rect.y, /*mTitleWithF + */0*(Mathf.Sin(Time.time*0.5f)*5), mTitle.sprite.rect.height);
    }
}
