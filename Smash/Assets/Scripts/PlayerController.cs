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
        GetComponentInChildren<SmashChargeFeedback>().SetController(this);
    }
    
    public void InitPosition() {
        m_position = 0;
    }

    public float GetPosition () {
        return m_position;
    }

    public bool IsSmashAvailable () {
        return m_isSmashAvailable;
    }

    public bool IsSmashTriggered() {
        return m_isSmashAvailable;
    }

    public int GetSmashCharge () {
        return m_smashCharge;
    }

    public void AddSmashCharge() {
        if (m_smashCharge < m_gameManager.SmashCost) {
            m_smashCharge += 1;
        }
        if (m_smashCharge == m_gameManager.SmashCost) {
            m_isSmashAvailable = true;
            m_isSmashTriggered = true;
        }
    }

    public void ConsumeSmash () {
        m_smashCharge = 0;
        m_isSmashAvailable = false;
        m_isSmashTriggered = false;
    }

    public Players GetPlayer () {
        return m_player;
    }

    Players m_player;
    GameManager m_gameManager;
    TouchManager m_touchManager;
    float m_position = 0;
    bool m_isSmashAvailable = false;
    bool m_isSmashTriggered = false;
    int m_smashCharge = 0;

    void Awake () {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_touchManager = GameObject.Find("TouchManager").GetComponent<TouchManager>();
    }

    void Update() {
        if (m_touchManager.HasTouch(m_player)) {
            m_position = Camera.main.ScreenToWorldPoint(m_touchManager.GetTouchPosition(m_player)).y;
        }
    }
}
