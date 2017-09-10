using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour {

    public void SetVelocity(Vector3 velocity) {
        m_velocity = velocity;
    }

    public Vector3 GetVelocity() {
        return m_velocity;
    }



    Vector3 m_velocity;
    float upperBound;
    float lowerBound;

    void Start() {
        upperBound = Camera.main.orthographicSize - transform.localScale.y / 2.0f;
        lowerBound = -upperBound;
    }

    void Update () {
        Move();
        CollideBounds();
    }

    void Move () {
        transform.position += m_velocity * Time.deltaTime;
    }

    void CollideBounds () {
        if (transform.position.y > upperBound || transform.position.y < lowerBound) {
            m_velocity.y *= -1;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Goal")) {
            Debug.Log("Velocity at Goal : " + m_velocity.x.ToString());
        }
    }
}
