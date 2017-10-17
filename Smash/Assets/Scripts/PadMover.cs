using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadMover : MonoBehaviour {
    

    public void SetController (IController controller) {
        m_controller = controller;
        //Debug.Log("Controller Set for " + transform.parent.name + " Pad Mover");
        //Debug.Log(m_controller);
    }

    
    IController m_controller;
    
    float xPosition;
    float zPosition;

    void Start () {
        xPosition = GameManager.Instance.RightBound - GameManager.Instance.TouchAreaWidth - 0.5f - (transform.lossyScale.x * 0.5f);
        if (m_controller.GetPlayer() == PlayerIds.P1) {
            xPosition *= -1;
        }
        zPosition = transform.position.z;
	}
	
	void Update () {
        transform.position = new Vector3(xPosition, m_controller.GetPosition(), zPosition);
    }
}
