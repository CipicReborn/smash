using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TouchManager : MonoBehaviour {

    public bool HasTouch (Players player) {
        if (player == Players.P1) {
            return player1TouchFound;
        }
        else {
            return player2TouchFound;
        }
    }

    public Vector2 GetTouchPosition (Players player) {
        if (player == Players.P1) {
            return player1.position;
        }
        else {
            return player2.position;
        }
    }

    RectTransform p1TouchArea;
    RectTransform p2TouchArea;

    Touch[] currentTouches;
    Touch player1;
    Touch player2;

    bool player1TouchFound = false;
    bool player2TouchFound = false;

    void Awake() {
        p1TouchArea = GameObject.Find("P1 Touch Area").GetComponent<RectTransform>();
        p2TouchArea = GameObject.Find("P2 Touch Area").GetComponent<RectTransform>();
    }

    bool IsInsideArea(Touch touch, RectTransform area) {
        Vector2 touchPositionUISpace;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(area, touch.position, null, out touchPositionUISpace);
        return area.rect.Contains(touchPositionUISpace);
    }
    void Start () {

    }
	
    void Stop () {

    }

	void Update () {
        player1TouchFound = false;
        player2TouchFound = false;
        currentTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++) {
            Touch currentTouch = currentTouches[i];
            if (!player1TouchFound && IsInsideArea(currentTouch, p1TouchArea)) {
                //Debug.Log("Got Player 1");
                player1 = currentTouch;
                player1TouchFound = true;
            }
            else if (!player2TouchFound && IsInsideArea(currentTouch, p2TouchArea)) {
                //Debug.Log("Got Player 2");
                player2 = currentTouch;
                player2TouchFound = true;
            }
            if (player1TouchFound && player2TouchFound) {
                break;
            }
        }
	}

    const int m_MAX_TOUCHES = 4;
    //Color[] FeedbackColors = new Color[m_MAX_TOUCHES] { Color.cyan, Color.red, Color.yellow, Color.green };

    //void OnDrawGizmos() {
    //    for (int i = 0; i < Input.touchCount; i++) {
    //        drawTouchFeedback(i);
    //    }
    //}

    //void drawTouchFeedback (int i) {
    //    if (i >= m_MAX_TOUCHES) {
    //        Debug.Log("Impossible to feedback this touch <" + i.ToString() + "> because max Touches count is " + m_MAX_TOUCHES.ToString());
    //        return;
    //    }
    //    Gizmos.color = FeedbackColors[i];
    //    Gizmos.DrawSphere(convertScreenPosToGamePos(currentTouches[i].position), 1);
    //}

    //Vector3 convertScreenPosToGamePos (Vector2 pPosition) {
    //    Vector3 position = new Vector3(pPosition.x, pPosition.y, -0.5f);
    //    return Camera.main.ScreenToWorldPoint(position);
    //}
}
