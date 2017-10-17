using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance {
        get { return instance; }
    }
    private static GameManager instance;

    #region PUBLIC MEMBERS

    public float InitialBallSpeed;
    public float MaxBallSpeed;
    public float SpeedIncrementPerStrike;
    public float SmashCost;
    public int TapCountToTriggerSmash;
    
    #endregion


    #region PUBLIC PROPERTIES

    internal GameTypes GameType {
        get { return m_gameType; }
        set { m_gameType = value; }
    }

    internal SoloModes SoloMode {
        get { return m_soloMode; }
        set { m_soloMode = value; }
    }

    internal AIs AI {
        get { return m_AI; }
        set { m_AI = value; }
    }

    public int RoundsCount {
        get { return m_roundsCount; }
        set { m_roundsCount = value; }
    }

    public int RoundsLength {
        get { return m_roundsLength; }
        set { m_roundsLength = value; }
    }

    public string CareerName {
        get { return m_careerName; }
        set { m_careerName = value; }
    }

    public float UpperBound { get { return m_upperBound; } }
    public float LowerBound { get { return m_lowerBound; } }
    public float LeftBound { get { return m_leftBound; } }
    public float RightBound { get { return m_rightBound; } }

    public float TouchAreaWidth {
        get { return m_touchAreaWidth; }
        set { m_touchAreaWidth = value; }
    }

    public PlayerTypes P1Type { get { return m_p1Type; } }
    public PlayerTypes P2Type { get { return m_p2Type; } }

    public Dictionary<PlayerIds, Player> Players { get { return m_players; } }

    public float TouchAreaWidthInPixels { get { return m_targetTouchAreaWidthInPixels; } }



    #endregion


    #region PUBLIC METHODS

    public void StartGame () {
        Debug.Log("Game Start");
        DoStartGame();
    }

    public void Add1PointToScore (PlayerIds id) {
        m_players[id].Score1Point();
        UpdateScoreDisplay();
        if (m_players[id].CurrentScore == m_roundsLength) {
            m_gameSequencer.EndRound();
            m_uIManager.OpenStartScreen();
        }
    }

    public void InitScore () {
        foreach (Player player in m_players.Values) {
            player.ResetScore();
        }
        
        UpdateScoreDisplay();
    }

    #endregion


    #region PRIVATE MEMBERS

    PlayerTypes m_p1Type = PlayerTypes.Human;
    PlayerTypes m_p2Type = PlayerTypes.Human;

    Dictionary<PlayerIds, Player> m_players;

    TouchManager m_TouchManager;
    UIManager m_uIManager;
    GameSequencer m_gameSequencer;

    GameTypes m_gameType;
    SoloModes m_soloMode;
    AIs m_AI;
    int m_roundsCount = 1;
    int m_roundsLength = 7;
    string m_careerName = "";

    float m_upperBound = 0;
    float m_lowerBound = 0;
    float m_leftBound = 0;
    float m_rightBound = 0;
    float m_touchAreaWidth = 0;

    const float m_INCH_2_CM = 2.54f;
    float m_targetTouchAreaWidthInPixels = 0;
    float m_targetWidthCm = 2.0f;

    Goal m_leftGoal;
    Goal m_rightGoal;

    #endregion


    #region PRIVATE METHODS

    void Awake() {
        instance = this;
        m_uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        m_gameSequencer = GameObject.Find("GameSequencer").GetComponent<GameSequencer>();
        m_TouchManager = GameObject.Find("TouchManager").GetComponent<TouchManager>();
        m_leftGoal = GameObject.Find("Left Goal").GetComponent<Goal>();
        m_rightGoal = GameObject.Find("Right Goal").GetComponent<Goal>();

        m_upperBound = Camera.main.orthographicSize;
        m_lowerBound = -m_upperBound;
        m_rightBound = (float) Screen.width / (float) Screen.height * Camera.main.orthographicSize;
        Debug.Log(Screen.width.ToString() + " / " + Screen.height.ToString() + " x " + Camera.main.orthographicSize.ToString());
        Debug.Log(m_rightBound.ToString());
        m_leftBound = -m_rightBound;

        DetermineTouchAreaSize();
        SetGoalsPosition();
        //Debug.Log("[GameManager] Upper Bound (" + m_upperBound.ToString() + "), Lower Bound (" + m_lowerBound.ToString() + ")");
    }

    void DoStartGame () {
        Debug.Log("[GameManager] Starting Game : " + m_gameType);
        m_players = new Dictionary<PlayerIds, Player>();
        if (m_gameType == GameTypes.Debug) {
            m_players[PlayerIds.P1] = new Player(PlayerIds.P1, PlayerTypes.CPU, "LeftPad");
        }
        else {
            m_players[PlayerIds.P1] = new Player(PlayerIds.P1, PlayerTypes.Human, "LeftPad");
        }
        if (m_gameType == GameTypes.Duo) {
            m_players[PlayerIds.P2] = new Player(PlayerIds.P2, PlayerTypes.Human, "RightPad");
        }
        else {
            m_players[PlayerIds.P2] = new Player(PlayerIds.P2, PlayerTypes.CPU, "RightPad");
        }
        m_TouchManager.InitGame();
        m_gameSequencer.StartRound();
    }

    void DoEndGame() {
        Debug.Log("[GameManager] Ending Game");
        m_players[PlayerIds.P1].Stop();
        m_players[PlayerIds.P2].Stop();
        m_TouchManager.StopGame();
        m_gameSequencer.EndRound();
    }

    void Update() {

    }

    void UpdateScoreDisplay () {
        m_uIManager.UpdateScore(m_players[PlayerIds.P1].CurrentScore, m_players[PlayerIds.P2].CurrentScore);
    }

    void DetermineTouchAreaSize () {

        var canvasScaler = GameObject.Find("UICanvas").GetComponent<UnityEngine.UI.CanvasScaler>();


        m_targetTouchAreaWidthInPixels = m_targetWidthCm / m_INCH_2_CM * Screen.dpi * canvasScaler.referenceResolution.x / Screen.width;
        m_touchAreaWidth = GetTouchAreaWidthWorldSpace();

        Debug.Log("Touch Area Width Set to [" + m_targetTouchAreaWidthInPixels.ToString() + " pixels]");
        Debug.Log("Touch Area Width Set to [" + m_touchAreaWidth.ToString() + " meters]");
    }

    float GetTouchAreaWidthWorldSpace () {

        var canvasScaler = GameObject.Find("UICanvas").GetComponent<UnityEngine.UI.CanvasScaler>();


        float pixelsPerMeter = canvasScaler.referenceResolution.y / (Camera.main.orthographicSize * 2.0f);
        Debug.Log(Screen.width + " x " + Screen.height);
        Debug.Log(canvasScaler.referenceResolution.x + " x " + canvasScaler.referenceResolution.y);
        Debug.Log("pixels per meter : " + pixelsPerMeter);
        return m_targetTouchAreaWidthInPixels / pixelsPerMeter;
    }

    void SetGoalsPosition () {
        m_leftGoal.ResetPosition(PlayerIds.P1);
        m_rightGoal.ResetPosition(PlayerIds.P2);
    }


    #endregion
}
