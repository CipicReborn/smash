using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequencer : MonoBehaviour {

    #region PUBLIC METHODS

    public void StartRound () {
        m_isRoundInProgress = true;
        m_isFirstPointOfRound = true;
        m_gameManager.InitScore();
        StartNewPoint();
    }

    public void EndRound() {
        m_isRoundInProgress = false;
        m_ball.Init();
        m_ball.SetVelocity(Vector3.zero);
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

    public void SetSmashAction (bool isSmashAction) {
        m_isSmashAction = isSmashAction;
    }

    public IEnumerator EndPoint (PlayerIds pointWinner, PlayerIds pointLoser) {
        m_ball.SetVelocity(Vector3.zero);
        if (m_isSmashAction) {
            yield return StartCoroutine(m_uiManager.DisplayBoom());
        }
        else {
            yield return null;
        }
        
        m_ball.Disable();
        m_gameManager.Add1PointToScore(pointWinner);
        
        if (m_isRoundInProgress) {
            StartNewPoint(pointLoser);
        }
    }

    #endregion

    #region PRIVATE MEMBERS

    GameManager m_gameManager;
    UIManager m_uiManager;
    bool m_isRoundInProgress = false;
    bool m_isFirstPointOfRound = false;
    bool m_isSmashAction = false;
    BallMover m_ball;
    GameObject m_leftPad;
    GameObject m_rightPad;
    Vector3 m_engagmentDirection;
    #endregion

    #region PRIVATE METHODS

    void Awake() {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        m_ball = GameObject.Find("Ball").GetComponent<BallMover>();
        m_leftPad = GameObject.Find("LeftPad");
        m_rightPad = GameObject.Find("RightPad");
    }

    void StartNewPoint(Vector3 direction) {
        m_engagmentDirection = direction;
        int countdown = 0;
        if (m_isFirstPointOfRound) {
            countdown = 3;
            m_isFirstPointOfRound = false;
        }
        else {
            countdown = 2;
        }
        StartCoroutine(m_uiManager.StartEngagementCountdown(countdown, Engage));
    }

    void Engage () {
        m_isSmashAction = false;
        m_ball.Init();
        m_leftPad.GetComponent<IController>().InitPoint();
        m_rightPad.GetComponent<IController>().InitPoint();
        m_ball.SetVelocity(m_engagmentDirection * m_gameManager.InitialBallSpeed);
        Debug.Log("Engagement Done By Game Sequencer");
    }

    #endregion
}
