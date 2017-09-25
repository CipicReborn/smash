using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reads the touch controller in order to calculate :
/// - pad position in world space
/// - smash input
/// </summary>
public class PlayerController : PadController {

    TouchManager m_touchManager;
    public float LerpSpeed = 0.5f;

    override public bool IsSmashTriggered() {   
        return m_isSmashTriggered;
    }

    override public void ConsumeSmash() {
        base.ConsumeSmash();
        m_isSmashTriggered = false;
    }

    override protected void Awake () {
        base.Awake();
        m_touchManager = GameObject.Find("TouchManager").GetComponent<TouchManager>();
    }

    void Update() {
        UpdatePosition();
        UpdateSmash();
    }

    void UpdatePosition () {
        if (m_touchManager.HasTouch(m_playerId)) {
            m_position = Mathf.Clamp(Mathf.Lerp(m_position, Camera.main.ScreenToWorldPoint(m_touchManager.GetTouchPosition(m_playerId)).y, LerpSpeed), m_gameManager.LowerBound + m_halfPadSize, m_gameManager.UpperBound - m_halfPadSize);
        }
    }

    void UpdateSmash () {
        if (!m_isSmashTriggered && m_smashCharge == m_gameManager.SmashCost && m_touchManager.GetTapCount(m_playerId) >= m_gameManager.TapCountToTriggerSmash) {
            m_isSmashTriggered = true;
        }
    }
}
