using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour {

    public void SetVelocity (Vector3 velocity) {
        m_velocity = velocity;
        m_useSmashVelocity = false;
        //Debug.Log("Ball Velocity set to " + m_velocity.ToString());
    }

    public void SetSmashVelocity (Vector3 velocity) {
        m_smashVelocity = velocity;
        m_useSmashVelocity = true;
        //Debug.Log("Ball Velocity set to " + m_velocity.ToString());
    }

    public Vector3 GetVelocity() {
        return m_velocity;
    }



    Vector3 m_velocity = Vector3.zero;
    Vector3 m_smashVelocity = Vector3.zero;
    bool m_useSmashVelocity = false;
    float m_upperBound = 0;
    float m_lowerBound = 0;

    void Awake() {
        m_upperBound = Camera.main.orthographicSize - transform.localScale.y / 2.0f;
        m_lowerBound = -m_upperBound;
    }

    void Update () {
        Move();
        CollideBounds();
    }

    void Move () {
        if (m_useSmashVelocity) {
            transform.position += m_smashVelocity * Time.deltaTime;
        }
        else {
            transform.position += m_velocity * Time.deltaTime;
        }
        //Debug.Log("Ball new position : " + transform.position.ToString());
    }

    void CollideBounds () {
        if (transform.position.y > m_upperBound || transform.position.y < m_lowerBound) {
            m_velocity.y *= -1;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Goal")) {
            Debug.Log("Velocity at Goal : " + m_velocity.x.ToString());
        }
    }
}
