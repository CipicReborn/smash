using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour, IController {

    public void Init (Players player) {
        m_player = player;
        GetComponent<PadMover>().SetController(this);
        GetComponent<BallStriker>().SetController(this);
        GetComponentInChildren<SmashChargeFeedback>().SetController(this);
    }

    public void InitPosition () {
        m_position = 0;
    }

    public Players GetPlayer () {
        return m_player;
    }

    public float GetPosition () {
        return m_position;
    }

    public bool IsSmashAvailable() {
        return m_isSmashAvailable;
    }

    public bool IsSmashTriggered() {
        return m_isSmashAvailable;
    }

    public int GetSmashCharge() {
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

    public void ConsumeSmash() {
        m_smashCharge = 0;
        m_isSmashAvailable = false;
        m_isSmashTriggered = false;
    }

    protected GameManager m_gameManager;
    protected Players m_player;
    protected float m_position;
    protected int m_smashCharge = 0;
    protected bool m_isSmashAvailable = false;
    protected bool m_isSmashTriggered = false;

    protected virtual void Awake() {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
