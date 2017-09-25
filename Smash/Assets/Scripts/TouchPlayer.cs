using UnityEngine;

public class TouchPlayer {

    #region PUBLIC PROPERTIES

    public bool TouchFound {
        get {
            return m_touchFound;
        }
    }

    public Touch Touch {
        get {
            return m_touch;
        }
    }

    public int TapCount {
        get {
            return m_tapCount;
        }
    }

    #endregion


    #region PUBLIC METHODS

    public TouchPlayer(Player pPlayer, float maxTapDuration, float maxTapInterval) {
        m_player = pPlayer;
        m_tapMaxDurationInSeconds = maxTapDuration;
        m_tapMaxIntervalInSeconds = maxTapInterval;
        SetTouchArea();
    }

    public void InitFrame() {
        m_touchFound = false;
        ResetTapCountOnIntervalExceeded();
    }

    public bool IsInsideArea(Touch touch) {
        Vector2 touchPositionUISpace;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_touchArea, touch.position, null, out touchPositionUISpace);
        return m_touchArea.rect.Contains(touchPositionUISpace);
    }

    public void RecordTouch(Touch touch) {
        //Debug.Log("Recording " + m_player.Id + " Touch");
        m_touch = touch;
        m_touchFound = true;
        InterpretTouch();
    }

    #endregion

    #region PRIVATE MEMBERS

    Player m_player;
    float m_tapMaxDurationInSeconds = 0;
    float m_tapMaxIntervalInSeconds = 0;

    RectTransform m_touchArea;
    Touch m_touch;
    bool m_touchFound = false;

    int m_tapCount = 0;
    float m_tapStartTime = 0;
    float m_tapEndTime = 0;
    bool m_isTapping = false;

    #endregion


    #region PRIVATE METHODS

    void SetTouchArea() {
        if (m_player.Type == PlayerTypes.Human) {
            m_touchArea = GameObject.Find(m_player.Id + " Touch Area").GetComponent<RectTransform>();
        }
        else if (m_player.Type == PlayerTypes.CPU) {
            m_touchArea = null;
        }
    }

    void ResetTapCountOnIntervalExceeded() {
        if (m_tapCount > 0 && Time.time - m_tapEndTime > m_tapMaxIntervalInSeconds) {
            m_tapCount = 0;
            //Debug.Log(m_player.Id + " TapCount = " + m_tapCount);
        }
    }

    public void InterpretTouch() {
        if (m_touch.phase == TouchPhase.Began) {
            m_tapStartTime = Time.time;
            m_isTapping = true;
        }
        else if (m_touch.phase == TouchPhase.Moved || m_touch.phase == TouchPhase.Stationary) {
            if (Time.time - m_tapStartTime > m_tapMaxDurationInSeconds) {
                m_isTapping = false;
                m_tapCount = 0;
                //Debug.Log(m_player.Id + " TapCount = " + m_tapCount);
            }
        }
        else if (m_touch.phase == TouchPhase.Ended || m_touch.phase == TouchPhase.Canceled) {
            m_tapEndTime = Time.time;
            if (m_isTapping) {
                m_tapCount++;
                //Debug.Log(m_player.Id + " TapCount = " + m_tapCount);
            }
            m_isTapping = false;
        }
    }

    #endregion
}
