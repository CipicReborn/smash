using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequencer : MonoBehaviour {

    GameManager m_gameManager;
    GameObject m_ball;
    GameObject m_leftPad;
    GameObject m_rightPad;

    Vector3 m_initialBallPosition;

    public void StartGame () {
        m_gameManager.InitScore();
        StartRound();
    }

    public void StartRound (Players engagingPlayer) {
        Vector3 engagementDirection;
        if (engagingPlayer == Players.P1) {
            engagementDirection = Vector3.left;
        }
        else {
            engagementDirection = Vector3.right;
        }
        Engage(engagementDirection);
    }

    public void StartRound() {
        Vector3 engagementDirection;
        int random = Random.Range(0, 2);
        if (random == 0) {
            engagementDirection = Vector3.left;
        }
        else {
            engagementDirection = Vector3.right;
        }
        Engage(engagementDirection);
    }
    
    public void EndRound (Players roundWinner, Players roundLoser) {
        m_gameManager.Add1PointToScore(roundWinner);
        StartRound(roundLoser);
    }

    
    void Awake() {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_ball = GameObject.Find("Ball");
        m_leftPad = GameObject.Find("LeftPad");
        m_rightPad = GameObject.Find("RightPad");
        
        m_initialBallPosition = m_ball.transform.position;
    }

    void Start() {

    }

    void Engage (Vector3 engagementDirection) {
        m_ball.transform.position = m_initialBallPosition;
        m_leftPad.GetComponent<IController>().InitPosition();
        m_rightPad.GetComponent<IController>().InitPosition();
        m_ball.GetComponent<BallMover>().SetVelocity(engagementDirection * m_gameManager.initialBallSpeed);
    }
}
