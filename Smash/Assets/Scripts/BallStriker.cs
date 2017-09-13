using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStriker : MonoBehaviour {

    public void SetController(IController controller) {
        m_controller = controller;
        UpdateStrikeVectors();
    }


    GameManager m_gameManager;
    IController m_controller;
    Transform m_ballTransform;
    BallMover m_ballMover;
    Vector3 m_toTop = new Vector3(1, 1, 0);
    Vector3 m_toBottom = new Vector3(1, -1, 0);

    void Awake() {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void UpdateStrikeVectors() {
        if (m_controller.GetPlayer() == Players.P1) {
            m_toTop.x = 1;
            m_toBottom.x = 1;
        }
        else if (m_controller.GetPlayer() == Players.P2) {
            m_toTop.x = -1;
            m_toBottom.x = -1;
        }
        m_toTop.Normalize();
        m_toBottom.Normalize();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            if (m_ballTransform == null) {
                m_ballTransform = other.gameObject.transform;
                m_ballMover = other.gameObject.GetComponent<BallMover>();
            }
            StrikeBall();
        }
    }

    void StrikeBall () {
        Vector3 newVelocity = Vector3.zero;

        ApplyDirection(m_ballTransform.position, out newVelocity);

        if (m_controller.IsSmashTriggered()) {
            ApplySmashVelocity(m_ballMover.GetVelocity(), ref newVelocity);
            m_ballMover.SetSmashVelocity(newVelocity);
            m_controller.ConsumeSmash();
        }
        else {
            ApplyVelocity(m_ballMover.GetVelocity(), ref newVelocity);
            m_ballMover.SetVelocity(newVelocity);
            m_controller.AddSmashCharge();
        }
    }

    void ApplyDirection (Vector3 position, out Vector3 direction) {
        float t = (transform.position.y + (transform.localScale.y / 2.0f) - position.y) / transform.localScale.y;
        t = Mathf.Clamp01(t);
        direction = Vector3.Slerp(m_toTop, m_toBottom, t).normalized;
    }

    void ApplyVelocity (Vector3 velocityIn, ref Vector3 normalisedVelocityOut) {
        float speed = 0;
        speed = velocityIn.magnitude * (1 + m_gameManager.SpeedIncrementPerStrike);
        speed = Mathf.Clamp(speed, 0f, m_gameManager.MaxBallSpeed);
        //Debug.Log("Ball New Speed : " + speed.ToString());
        normalisedVelocityOut *= speed;
    }

    void ApplySmashVelocity(Vector3 velocityIn, ref Vector3 normalisedVelocityOut) {
        normalisedVelocityOut *= m_gameManager.MaxBallSpeed;
    }

}
