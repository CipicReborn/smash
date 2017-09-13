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

    protected override void Awake () {
        base.Awake();
        m_touchManager = GameObject.Find("TouchManager").GetComponent<TouchManager>();
    }

    void Update() {
        if (m_touchManager.HasTouch(m_player)) {
            m_position = Camera.main.ScreenToWorldPoint(m_touchManager.GetTouchPosition(m_player)).y;
        }
    }
}
