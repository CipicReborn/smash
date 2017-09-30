using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequencer : MonoBehaviour {

    GameManager m_gameManager;
    BallMover m_ball;
    GameObject m_leftPad;
    GameObject m_rightPad;
    bool m_isRoundInProgress = false;

    Vector3 m_initialBallPosition;

    public void StartRound () {
        m_isRoundInProgress = true;
        m_gameManager.InitScore();
        StartNewPoint();
    }

    public void EndRound() {
        m_isRoundInProgress = false;
        m_ball.transform.position = m_initialBallPosition;
        m_ball.SetVelocity(Vector3.zero);
    }

    public void StartNewPoint(PlayerIds engagingPlayer) {
        Vector3 engagementDirection;
        if (engagingPlayer == PlayerIds.P1) {
            engagementDirection = Vector3.left;
        }
        else {
            engagementDirection = Vector3.right;
        }
        StartNewPoint(engagementDirection);
    }

    public void StartNewPoint() {
        Vector3 engagementDirection;
        int random = Random.Range(0, 2);
        if (random == 0) {
            engagementDirection = Vector3.left;
        }
        else {
            engagementDirection = Vector3.right;
        }
        StartNewPoint(engagementDirection);
    }
    
    void StartNewPoint (Vector3 direction) {

        Engage(direction);
    }

    public void EndPoint (PlayerIds pointWinner, PlayerIds pointLoser) {
        m_gameManager.Add1PointToScore(pointWinner);
        if (m_isRoundInProgress) {
            StartNewPoint(pointLoser);
        }
    }

    
    void Awake() {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_ball = GameObject.Find("Ball").GetComponent<BallMover>();
        m_leftPad = GameObject.Find("LeftPad");
        m_rightPad = GameObject.Find("RightPad");
    }

    void Start() {

    }

    void Engage (Vector3 engagementDirection) {
        m_ball.InitPosition();
        m_leftPad.GetComponent<IController>().InitPoint();
        m_rightPad.GetComponent<IController>().InitPoint();
        m_ball.SetVelocity(engagementDirection * m_gameManager.InitialBallSpeed);
        Debug.Log("Engagement Done By Game Sequencer");
    }
}
