using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : PadController {

    #region PUBLIC MEMBERS

    public float LerpSpeed = 0.5f; //Speed of the lerp to target position
    public const float m_OFFSET_APPLICATION_DISTANCE_THRESHOLD = 2; //Distance to the ball at which the reception position starts to be applied
    public float m_maxOffset = 1; // maximum offset to pad center when striking ball
    
    #endregion

    #region PUBLIC METHODS

    override public void Init (PlayerIds player) {
        base.Init(player); //reference the pad owner
        DetermineOpponentDirection();
    }

    #endregion

    #region PRIVATE MEMBERS

    GameObject m_ball;
    GameObject m_opponentPad;
    bool m_isReceptionPhase = false;
    Vector3 m_opponentDirection = Vector3.zero;
    float m_strikeOffset
        ;
    #endregion PRIVATE MEMBERS

    #region PRIVATE METHODS

    override protected void Awake() {
        base.Awake();
        ReferenceOpponentPad();
        ReferenceBall();
    }

    void ReferenceOpponentPad() {
        GameObject[] pads = GameObject.FindGameObjectsWithTag("Pad");
        for (int i = 0; i < pads.Length; i++) {
            if (pads[i] != gameObject) {
                m_opponentPad = pads[i];
                break;
            }
        }
    }

    void ReferenceBall () {
        m_ball = GameObject.Find("Ball");
    }

    void DetermineOpponentDirection () {
        if (m_playerId == PlayerIds.P1) {
            m_opponentDirection = Vector3.right;
        }
        else if (m_playerId == PlayerIds.P2) {
            m_opponentDirection = Vector3.left;
        }
    }

    private void Start() {}

    void Update() {
        DeterminePhase();
        if (m_isReceptionPhase) {
            MoveReceptionPhase();
        }
        else {
            MoveWaitPhase();
        }
    }

    void DeterminePhase () {
        float dotProduct = Vector3.Dot(m_ball.GetComponent<BallMover>().GetVelocity(), m_opponentDirection);
        if (!m_isReceptionPhase && dotProduct < 0) {
            m_isReceptionPhase = true;
            DoPhaseSwitchOperations();
        }
        if (m_isReceptionPhase && dotProduct > 0) {
            m_isReceptionPhase = false;
            DoPhaseSwitchOperations();
        }
    }

    void DoPhaseSwitchOperations () {
        if (m_isReceptionPhase) {
            LerpSpeed = 0.5f;
            DetermineStrikeOffset();
        }
        else {
            LerpSpeed = 0.1f;
        }
    }
    
    void DetermineStrikeOffset () {
        m_strikeOffset = Random.Range(-m_maxOffset, m_maxOffset);
    }

    void MoveReceptionPhase() {
        float targetPosition = m_ball.transform.position.y;
        if (m_isReceptionPhase && (m_ball.transform.position - transform.position).magnitude < m_OFFSET_APPLICATION_DISTANCE_THRESHOLD) {
            targetPosition += m_strikeOffset;
        }
        LerpToPosition(targetPosition);
    }
    
    void MoveWaitPhase () {
        float targetPosition = Mathf.Cos(Time.time);
        LerpToPosition(targetPosition);
    }

    void LerpToPosition (float targetPosition) {
        m_position = Mathf.Clamp(Mathf.Lerp(m_position, targetPosition, LerpSpeed), m_gameManager.LowerBound + m_halfPadSize, m_gameManager.UpperBound - m_halfPadSize);
    }

    #endregion
}
