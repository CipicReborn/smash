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
    AudioSource m_ballSfx;
    Vector3 m_toTop = new Vector3(1, 1, 0);
    Vector3 m_toBottom = new Vector3(1, -1, 0);
    AudioClip m_sfxHit;
    AudioClip m_sfxSmash;
    ParticleSystem m_particles;
    Animator m_gfxAnimator;

    void Awake() {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        m_ballTransform = GameObject.Find("Ball").transform;
        m_ballMover = GameObject.Find("Ball").GetComponent<BallMover>();
        m_ballSfx = GameObject.Find("Ball").GetComponent<AudioSource>();
        m_sfxHit = Resources.Load("hitball") as AudioClip;
        m_sfxSmash = Resources.Load("smashball") as AudioClip;

        m_particles = GetComponentInChildren<ParticleSystem>();
        m_gfxAnimator = GetComponentInChildren<Animator>();
    }

    void UpdateStrikeVectors() {
        if (m_controller.GetPlayer() == PlayerIds.P1) {
            m_toTop.x = 1;
            m_toBottom.x = 1;
        }
        else if (m_controller.GetPlayer() == PlayerIds.P2) {
            m_toTop.x = -1;
            m_toBottom.x = -1;
        }
        m_toTop.Normalize();
        m_toBottom.Normalize();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            StrikeBall();
            PlayParticlesFX(new Vector3(m_particles.transform.localPosition.x, other.gameObject.transform.position.y - transform.position.y, 0));
        }
    }

    void StrikeBall () {
        Vector3 newVelocity = Vector3.zero;

        ApplyDirection(m_ballTransform.position, out newVelocity);

        if (m_controller.IsSmashTriggered()) {
            ApplySmashVelocity(m_ballMover.GetVelocity(), ref newVelocity);
            m_ballMover.SetSmashVelocity(newVelocity);
            m_controller.ConsumeSmash();
            m_ballSfx.clip = m_sfxSmash;
            m_gfxAnimator.SetBool("isSmashing", true);
        }
        else {
            ApplyVelocity(m_ballMover.GetVelocity(), ref newVelocity);
            m_ballMover.SetVelocity(newVelocity);
            m_controller.AddSmashCharge();
            m_ballSfx.clip = m_sfxHit;
        }
        m_ballSfx.pitch = 1 + Mathf.Round(Random.Range(-0.1f, 0.1f) * 100.0f)/100.0f;
        m_ballSfx.Play();
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

    void PlayParticlesFX(Vector3 localPos) {
        m_particles.transform.localPosition = localPos;
        m_particles.Play();
    }
}
