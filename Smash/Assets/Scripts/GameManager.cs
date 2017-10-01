using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region PUBLIC MEMBERS

    public float InitialBallSpeed;
    public float MaxBallSpeed;
    public float SpeedIncrementPerStrike;
    public float SmashCost;
    public int TapCountToTriggerSmash;
    
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

    public PlayerTypes P1Type {
        get {
            return m_p1Type;
        }
    }

    public PlayerTypes P2Type {
        get {
            return m_p2Type;
        }
    }

    public Dictionary<PlayerIds, Player> Players {
        get {
            return m_players;
        }
    }

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



    #endregion


    #region PRIVATE METHODS

    void Awake() {
        m_uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        m_gameSequencer = GameObject.Find("GameSequencer").GetComponent<GameSequencer>();
        m_TouchManager = GameObject.Find("TouchManager").GetComponent<TouchManager>();
        
        m_upperBound = Camera.main.orthographicSize;
        m_lowerBound = -m_upperBound;
        //Debug.Log("[GameManager] Upper Bound (" + m_upperBound.ToString() + "), Lower Bound (" + m_lowerBound.ToString() + ")");
    }

    void DoStartGame () {
        Debug.Log("[GameManager] Starting Game");
        m_players = new Dictionary<PlayerIds, Player>();
        m_players[PlayerIds.P1] = new Player(PlayerIds.P1, PlayerTypes.CPU, "LeftPad");
        m_players[PlayerIds.P2] = new Player(PlayerIds.P2, PlayerTypes.CPU, "RightPad");
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

    #endregion
}
