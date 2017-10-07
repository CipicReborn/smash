using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {


    private float m_shakeAmount;
    public float smoothTimeX;
    public float smoothTimeY;

    private float m_shakeTimer = 0;

    private Vector3 targetPosition = Vector3.zero;
    private Vector2 velocity = Vector2.zero;

    /// <summary>
    /// Camera Follow
    /// </summary>
    private void FixedUpdate() {

        float posX = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, targetPosition.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    private void Update() {
        if (m_shakeTimer >= 0) {
            Vector2 shakePos = Random.insideUnitCircle * m_shakeAmount;
            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
            m_shakeTimer -= Time.deltaTime;
        }
    }

    public void ShakeCameraForSeconds (float duration, float magnitude) {
        m_shakeTimer = duration;
        m_shakeAmount = magnitude;
    }
}
