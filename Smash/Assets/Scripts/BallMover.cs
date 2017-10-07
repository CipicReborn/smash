using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallMover : MonoBehaviour {

    public ReboundParticlesController particles;

    public void Init() {
        transform.position = m_initialPosition;
        GetComponentInChildren<LineRendererController>().Reset();
        m_smashTrail.SetActive(false);
    }

    public void SetVelocity (Vector3 velocity) {
        m_velocity = velocity;
        m_useSmashVelocity = false;
        m_smashTrail.SetActive(false);
        //Debug.Log("Ball Velocity set to " + m_velocity.ToString());
    }

    public void SetSmashVelocity (Vector3 velocity) {
        m_smashVelocity = velocity;
        m_useSmashVelocity = true;
        m_smashTrail.SetActive(true);
        //Debug.Log("Ball Velocity set to " + m_velocity.ToString());
    }

    public Vector3 GetVelocity() {
        return m_velocity;
    }


    GameManager m_gameManager;
    Vector3 m_initialPosition;
    AudioSource m_sfxPlayer;
    AudioClip m_hitWall;
    Vector3 m_velocity = Vector3.zero;
    Vector3 m_smashVelocity = Vector3.zero;
    bool m_useSmashVelocity = false;
    GameObject m_smashTrail;

    void Awake() {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_sfxPlayer = GetComponent<AudioSource>();
        m_hitWall = Resources.Load("hitwall") as AudioClip;
        m_initialPosition = transform.position;
        m_smashTrail = transform.Find("SmashTrail").gameObject;
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
        if (transform.position.y > (m_gameManager.UpperBound - transform.localScale.y / 2.0f)) {
            m_velocity.y = -Mathf.Abs(m_velocity.y);
            m_smashVelocity.y = -Mathf.Abs(m_smashVelocity.y);
            particles.BurstDown(new Vector3(transform.position.x, m_gameManager.UpperBound, transform.position.z));
            PlayHitSound();
        }
        else if (transform.position.y < (m_gameManager.LowerBound + transform.localScale.y / 2.0f )) {
            m_velocity.y = Mathf.Abs(m_velocity.y);
            m_smashVelocity.y = Mathf.Abs(m_smashVelocity.y);
            particles.BurstUp(new Vector3(transform.position.x, m_gameManager.LowerBound, transform.position.z));
            PlayHitSound();
        }
    }

    void PlayHitSound () {
        m_sfxPlayer.clip = m_hitWall;
        m_sfxPlayer.pitch = 1 + Mathf.Round(Random.Range(-0.1f, 0.1f) * 100.0f) / 100.0f;
        m_sfxPlayer.Play();
    }

    //private void OnTriggerEnter(Collider other) {
    //    if (other.gameObject.CompareTag("Goal")) {
    //        Debug.Log("Velocity at Goal : " + m_velocity.x.ToString());
    //    }
    //}

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        if (m_useSmashVelocity) {
            Gizmos.DrawLine(transform.position, transform.position + m_smashVelocity.normalized);
        }
        else {
            Gizmos.DrawLine(transform.position, transform.position + m_velocity.normalized);
        }
    }
}
