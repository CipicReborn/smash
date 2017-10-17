using UnityEngine;
using UnityEngine.UI;

public class TouchAreaResizer : MonoBehaviour {

    void Start () {
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(GameManager.Instance.TouchAreaWidthInPixels, rt.sizeDelta.y);
    }




    //private void OnGUI() {
    //    GUI.Label(new Rect(25, 25, 200, 30), "ScreenWidth:" + Screen.width);
    //    GUI.Label(new Rect(25, 55, 200, 30), "TargetWidthPixels:" + m_targetWidthPixels);
    //}
}
