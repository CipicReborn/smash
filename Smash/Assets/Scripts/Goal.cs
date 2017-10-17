using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    GameSequencer m_gameSequencer;
    PlayerIds m_scoringPlayer;
    PlayerIds m_defendingPlayer;

	void Start () {
        m_gameSequencer = GameObject.Find("GameSequencer").GetComponent<GameSequencer>();
        
        if (transform.position.x < 0) {
            m_defendingPlayer = PlayerIds.P1;
            m_scoringPlayer = PlayerIds.P2;
        }
        else {
            m_defendingPlayer = PlayerIds.P2;
            m_scoringPlayer = PlayerIds.P1;
        }
        ResetPosition();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            StartCoroutine(m_gameSequencer.EndPoint(m_scoringPlayer, m_defendingPlayer));
        }
    }

    void ResetPosition () {
        float xPosition = GameManager.Instance.RightBound - GameManager.Instance.TouchAreaWidth - (transform.lossyScale.x * 0.5f);
        if (m_defendingPlayer == PlayerIds.P1) {
            xPosition *= -1;
        }
        transform.position = new Vector3 (xPosition, transform.position.y, transform.position.z);
    }
}
