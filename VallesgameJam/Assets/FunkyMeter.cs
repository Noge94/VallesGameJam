using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FunkyMeter : MonoBehaviour {
    public List<Sprite> funkyLevels;
    public BPM bpmInstance;
    public float hue = 0;
    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        int level = bpmInstance.getmCurrentBG();
        setFunkyLevel(level);
        if (level >= 3)
        {
            hue += 5;
            this.GetComponent<Image>().color = Color.HSVToRGB(hue / 256.0f, 0.5f, 1.0f);
            if (hue >= 256.0f)
                hue = 0.0f;
        }
        else {
            this.GetComponent<Image>().color = Color.white;
        }
    }

    public void setFunkyLevel(int level) {
        this.GetComponent<Image>().sprite = funkyLevels[level];
    }
}
