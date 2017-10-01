using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchAreaResizer : MonoBehaviour {

    const float m_INCH_2_CM = 2.54f;
    float m_targetWidthCm = 2.0f;
    float m_targetWidthPixels;
    CanvasScaler m_canvasScaler;

    void Start () {
        m_canvasScaler = GetComponentInParent<CanvasScaler>();
        m_targetWidthPixels = m_targetWidthCm / m_INCH_2_CM * Screen.dpi * m_canvasScaler.referenceResolution.x / Screen.width;
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(m_targetWidthPixels, rt.sizeDelta.y);
    }

    //private void OnGUI() {
    //    GUI.Label(new Rect(25, 25, 200, 30), "ScreenWidth:" + Screen.width);
    //    GUI.Label(new Rect(25, 55, 200, 30), "TargetWidthPixels:" + m_targetWidthPixels);
    //}
}
