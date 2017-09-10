using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    GameSequencer m_gameSequencer;
    Players m_scoringPlayer;
    Players m_defendingPlayer;

	void Start () {
        m_gameSequencer = GameObject.Find("GameSequencer").GetComponent<GameSequencer>();
        if (transform.position.x < 0) {
            m_defendingPlayer = Players.P1;
            m_scoringPlayer = Players.P2;
        }
        else {
            m_defendingPlayer = Players.P2;
            m_scoringPlayer = Players.P1;
        }
    }
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            m_gameSequencer.EndRound(m_scoringPlayer, m_defendingPlayer);
        }
    }
}
