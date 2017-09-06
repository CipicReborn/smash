using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {
    Touch[] currentTouches;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++) {
            Debug.Log(currentTouches[i].fingerId + " : " + currentTouches[i].position);
        }
        
	}
}
