using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region PUBLIC MEMBERS

    public float InitialBallSpeed;
    public float MaxBallSpeed;
    public float SpeedIncrementPerStrike;
    public float SmashCost;
    
    #endregion


    #region PUBLIC PROPERTIES

    internal GameTypes GameType {
        get {
            return m_gameType;
        }

        set {
            m_gameType = value;
        }
    }

    internal SoloModes SoloMode {
        get {
            return m_soloMode;
        }

        set {
            m_soloMode = value;
        }
    }

    internal AIs AI {
        get {
            return m_AI;
        }

        set {
            m_AI = value;
        }
    }

    public int RoundsCount {
        get {
            return m_roundsCount;
        }

        set {
            m_roundsCount = value;
        }
    }

    public int RoundsLength {
        get {
            return m_roundsLength;
        }

        set {
            m_roundsLength = value;
        }
    }

    public string CareerName {
        get {
            return m_careerName;
        }

        set {
            m_careerName = value;
        }
    }

    public float UpperBound {
        get {
            return m_upperBound;
        }
    }

    public float LowerBound {
        get {
            return m_lowerBound;
        }
    }

    #endregion


    #region PUBLIC METHODS

    public void StartGame () {
        Debug.Log("Game Start");
    }

    public void Add1PointToScore (Players player) {
        if (player == Players.P1) {
            currentGameP1Score += 1;
        }
        else if (player == Players.P2) {
            currentGameP2Score += 1;
        }
        UpdateScoreDisplay();
    }

    public void InitScore () {
        currentGameP1Score = 0;
        currentGameP2Score = 0;
        UpdateScoreDisplay();
    }

    #endregion


    #region PRIVATE MEMBERS

    UIManager m_uIManager;
    GameSequencer m_gameSequencer;
    GameTypes m_gameType;
    SoloModes m_soloMode;
    AIs m_AI;
    int m_roundsCount = 0;
    int m_roundsLength = 0;
    string m_careerName = "";
    int currentGameP1Score = 0;
    int currentGameP2Score = 0;

    float m_upperBound = 0;
    float m_lowerBound = 0;

    #endregion


    #region PRIVATE METHODS

    void Awake() {
        m_uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        m_gameSequencer = GameObject.Find("GameSequencer").GetComponent<GameSequencer>();

        m_upperBound = Camera.main.orthographicSize;
        m_lowerBound = -m_upperBound;
        Debug.Log("[GameManager] Upper Bound (" + m_upperBound.ToString() + "), Lower Bound (" + m_lowerBound.ToString() + ")");
    }

    void Start() {
        GameObject.Find("LeftPad").AddComponent<IAController>().Init(Players.P1);
        GameObject.Find("RightPad").AddComponent<IAController>().Init(Players.P2);
        m_gameSequencer.StartGame();
    }

    void Update() {

    }

    void UpdateScoreDisplay () {
        m_uIManager.UpdateScore(currentGameP1Score, currentGameP2Score);
    }

    #endregion
}
