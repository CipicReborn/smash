﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadMover : MonoBehaviour {
    

    public void SetController (IController controller) {
        m_controller = controller;
    }

    
    IController m_controller;
    
    float xPosition;
    float zPosition;

    void Start () {
        xPosition = transform.position.x;
        zPosition = transform.position.z;
	}
	
	void Update () {
        transform.position = new Vector3(xPosition, m_controller.GetPosition(), zPosition);
    }
}