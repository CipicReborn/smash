using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAreaResizer : MonoBehaviour {

    const float INCH_2_CM = 2.54f;
    float targetWidthCm = 2.0f;

	// Use this for initialization
	void Start () {
        float ScreenWidth = Screen.width / Screen.dpi * INCH_2_CM;
        float targetWidthPixels = targetWidthCm / INCH_2_CM * Screen.dpi;
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(targetWidthPixels, rt.sizeDelta.y);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
