using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashTriggerFeedback : MonoBehaviour {

    public void SetController (IController controller) {
        m_controller = controller;
    }

    ParticleSystem m_particleSystem;
    IController m_controller;

    void Start () {
        m_particleSystem = GetComponent<ParticleSystem>();
	}
	
	
	void Update () {
		if (m_controller.IsSmashTriggered() && !m_particleSystem.isEmitting) {
            m_particleSystem.Play();
        }
        else if (!m_controller.IsSmashTriggered() && m_particleSystem.isEmitting) {
            m_particleSystem.Stop();
        }
	}
}
