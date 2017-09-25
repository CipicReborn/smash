using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour, IController {

    virtual public void Init (PlayerIds playerId) {

        Debug.Log("[PadController] Init Game");

        SetComponentsEnabled(true);

        m_playerId = playerId;
        m_padMover.SetController(this);
        m_ballStriker.SetController(this);
        m_smashChargeFeedback.SetController(this);
        m_smashTriggerFeedback.SetController(this);

        
    }

    public void Stop () {
        SetComponentsEnabled(false);
    }

    public void InitPoint () {
        Debug.Log("[PadController] Init Point");
        m_position = 0;
        m_isSmashTriggered = false;
    }

    public PlayerIds GetPlayer () {
        return m_playerId;
    }

    public float GetPosition () {
        return m_position;
    }

    public bool IsSmashAvailable() {
        return m_isSmashAvailable;
    }

    virtual public bool IsSmashTriggered() {
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
        }
    }

    virtual public void ConsumeSmash() {
        m_smashCharge = 0;
        m_isSmashAvailable = false;
    }

    protected GameManager m_gameManager;

    PadMover m_padMover;
    BallStriker m_ballStriker;
    SmashChargeFeedback m_smashChargeFeedback;
    SmashTriggerFeedback m_smashTriggerFeedback;

    protected PlayerIds m_playerId;
    protected float m_position;
    protected int m_smashCharge = 0;
    protected bool m_isSmashAvailable = false;
    protected bool m_isSmashTriggered = false;
    protected float m_halfPadSize = 0;

    protected virtual void Awake() {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        ReferencePadSize();
        ReferenceComponents();

        SetComponentsEnabled(false);
    }

    void ReferencePadSize () {
        m_halfPadSize = GetComponent<BoxCollider>().size.y / 2;
    }

    void ReferenceComponents () {
        m_padMover = GetComponent<PadMover>();
        m_ballStriker = GetComponent<BallStriker>();
        m_smashChargeFeedback = GetComponentInChildren<SmashChargeFeedback>();
        m_smashTriggerFeedback = GetComponentInChildren<SmashTriggerFeedback>();
    }

    void SetComponentsEnabled (bool enabled) {
        m_padMover.enabled = enabled;
        m_ballStriker.enabled = enabled;
        m_smashChargeFeedback.enabled = enabled;
        m_smashTriggerFeedback.enabled = enabled;
    }

}
