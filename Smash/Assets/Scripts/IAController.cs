using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : PadController {

    public float LerpSpeed = 0.5f;

    override public void Init(Players player) {
        base.Init(player);
        if (m_player == Players.P1) {
            m_opponentDirection = Vector3.right;
        }
        else if(m_player == Players.P2) {
            m_opponentDirection = Vector3.left;
        }
    }

    GameObject m_ball;
    GameObject m_opponentPad;
    bool m_isReceptionPhase = false;
    Vector3 m_opponentDirection = Vector3.zero;

    override protected void Awake() {
        base.Awake();
        GameObject[] pads = GameObject.FindGameObjectsWithTag("Pad");
        for (int i = 0; i < pads.Length; i++) {
            if (pads[i] != gameObject) {
                m_opponentPad = pads[i];
                break;
            }
        }
    }

    private void Start() {
        m_ball = GameObject.Find("Ball");
    }

    void Update() {
        DeterminePhase();
        Move();
    }

    void DeterminePhase () {
        float dotProduct = Vector3.Dot(m_ball.GetComponent<BallMover>().GetVelocity(), m_opponentDirection);
        if (!m_isReceptionPhase && dotProduct < 0) {
            m_isReceptionPhase = true;
        }
        if (m_isReceptionPhase && dotProduct > 0) {
            m_isReceptionPhase = false;
        }
    }

    void Move () {
        float offset = Random.Range(-0.5f, 0.5f);
        m_position = Mathf.Clamp(Mathf.Lerp(m_position, m_ball.transform.position.y, LerpSpeed), m_gameManager.LowerBound + m_halfPadSize, m_gameManager.UpperBound - m_halfPadSize);
    }
}
