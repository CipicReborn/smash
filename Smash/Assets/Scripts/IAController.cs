using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : PadController {

    public float LerpSpeed = 0.5f;

    GameObject m_ball;
    float halfPadSize = 0.5f;

    private void Start() {
        m_ball = GameObject.Find("Ball");
    }

    void Update() {
        float offset = Random.Range(-0.5f, 0.5f);
        m_position = Mathf.Clamp(Mathf.Lerp(m_position, m_ball.transform.position.y, LerpSpeed),m_gameManager.LowerBound + halfPadSize, m_gameManager.UpperBound - halfPadSize);
    }
}
