using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {
    Touch[] currentTouches;
    
	void Start () {
		
	}
	
	void Update () {
        currentTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++) {
            //Debug.Log(currentTouches[i].fingerId + " : " + currentTouches[i].position);
            //drawTouchFeedback(i);
            //if (currentTouches[i].phase == TouchPhase.Ended) {

            //}
        }
        
	}

    const int m_MAX_TOUCHES = 4;
    Color[] FeedbackColors = new Color[m_MAX_TOUCHES] { Color.cyan, Color.red, Color.yellow, Color.green };

    void OnDrawGizmos() {
        for (int i = 0; i < Input.touchCount; i++) {
            drawTouchFeedback(i);
        }
    }

    void drawTouchFeedback (int i) {
        if (i >= m_MAX_TOUCHES) {
            Debug.Log("Impossible to feedback this touch <" + i.ToString() + "> because max Touches count is " + m_MAX_TOUCHES.ToString());
            return;
        }
        Gizmos.color = FeedbackColors[i];
        Gizmos.DrawSphere(convertScreenPosToGamePos(currentTouches[i].position), 1);
    }

    Vector3 convertScreenPosToGamePos (Vector2 pPosition) {
        Vector3 position = new Vector3(pPosition.x, pPosition.y, -0.5f);
        return Camera.main.ScreenToWorldPoint(position);
    }
}
