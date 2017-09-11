using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashChargeFeedback : MonoBehaviour {

    #region PUBLIC

    public void SetController(IController controller) {
        m_controller = controller;
    }

    #endregion


    #region PRIVATE
    Color m_indicatorOn;
    IController m_controller;
    int m_latestChargeCount = 0;



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
            for (int i = 0; i < m_latestChargeCount; i++) {
                transform.GetChild(i).GetComponent<MeshRenderer>().material.color = m_indicatorOn;
            }
        }
    }
    #endregion
}
