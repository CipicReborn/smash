using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    #region PUBLIC METHODS
    
    public void SelectSoloGameType () {
        m_gameManager.GameType = GameTypes.Solo;
    }

    public void SelectDuoGameType () {
        m_gameManager.GameType = GameTypes.Duo;
    }

    public void SelectEasyAI () {
        m_gameManager.AI = AIs.Easy;
    }

    public void SelectMediumAI () {
        m_gameManager.AI = AIs.Medium;
    }

    public void SelectHardAI () {
        m_gameManager.AI = AIs.Hard;
    }

    public void SelectSimpleMode () {
        m_gameManager.SoloMode = SoloModes.Simple;
    }

    public void SelectCareerMode () {
        m_gameManager.SoloMode = SoloModes.Career;
    }

    public void SelectSurvivalMode () {
        m_gameManager.SoloMode = SoloModes.Survival;
    }

    public void SetGameSettings (int roundsCount, int roundsLength) {
        m_gameManager.RoundsCount = roundsCount;
        m_gameManager.RoundsLength = roundsLength;
    }

    public void SetCareerName (string name) {
        m_gameManager.CareerName = name;
    }

    public void StartGame () {
        m_gameManager.StartGame();
    }

    #endregion


    #region PRIVATE MEMBERS

    GameManager m_gameManager;

    #endregion


    #region PRIVATE METHODS

    void Awake () {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    

    #endregion
}
