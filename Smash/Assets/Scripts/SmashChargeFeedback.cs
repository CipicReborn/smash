using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashChargeFeedback : MonoBehaviour {

    #region PUBLIC

    public void SetController(IController controller) {
        m_controller = controller;
        //Debug.Log("Controller Set for " + transform.parent.name + " Smash Charge");
        //Debug.Log(m_controller);
    }

    #endregion


    #region PRIVATE
    GameManager m_gameManager;
    Color m_indicatorOn;
    IController m_controller;
    int m_latestChargeCount = 0;


    void Awake () {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

	void Start () {
        m_indicatorOn = transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
	
	void Update () {
        UpdateCharges();
    }
    
    void UpdateCharges () {
        if (m_latestChargeCount != m_controller.GetSmashCharge()) {
            m_latestChargeCount = m_controller.GetSmashCharge();
            for (int i = 0; i < transform.childCount; i++) {
                if (i < m_latestChargeCount) {
                    transform.GetChild(i).GetComponent<MeshRenderer>().material.color = m_indicatorOn;
                }
                else {
                    transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
        }
    }
    #endregion
}
