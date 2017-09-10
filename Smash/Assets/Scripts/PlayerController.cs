using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reads the touch controller in order to calculate :
/// - pad position in world space
/// - smash input
/// </summary>
public class PlayerController : MonoBehaviour, IController {

    public void Init (Players player) {
        m_player = player;
        GetComponent<PadMover>().SetController(this);
        GetComponent<BallStriker>().SetController(this);
    }
    
    public void InitPosition() {
        m_position = 0;
    }

    public float GetPosition () {
        return m_position;
    }

    public bool GetSmash () {
        return m_smash;
    }

    public Players GetPlayer () {
        return m_player;
    }

    Players m_player;
    TouchManager m_touchManager;
    float m_position = 0;
    bool m_smash = false;

    void Awake () {
        m_touchManager = GameObject.Find("TouchManager").GetComponent<TouchManager>();
    }

    void Update() {
        m_position = Camera.main.ScreenToWorldPoint(m_touchManager.GetTouchPosition(m_player)).y;
    }
}
