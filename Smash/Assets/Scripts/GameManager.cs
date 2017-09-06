using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

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

    #endregion


    #region PUBLIC METHODS

    public void StartGame () {
        Debug.Log("Game Start");
    }

    #endregion


    #region PRIVATE MEMBERS

    GameTypes m_gameType;
    SoloModes m_soloMode;
    AIs m_AI;
    int m_roundsCount;
    int m_roundsLength;
    string m_careerName;



    #endregion


    #region PRIVATE METHODS

    void Start() {

    }

    void Update() {

    }

    #endregion
}
