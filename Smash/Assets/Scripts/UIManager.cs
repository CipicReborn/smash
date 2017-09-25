using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    #region PUBLIC METHODS

    public void UpdateScore (int p1Score, int p2Score) {
        m_scoreLabel.text = p1Score.ToString("D2") + " - " + p2Score.ToString("D2");
    }

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
        SetStateGameScreen();
        m_gameManager.StartGame();
    }

    #endregion


    #region PRIVATE MEMBERS

    GameManager m_gameManager;
    Text m_scoreLabel;
    Button m_startGameButton;
    #endregion


    #region PRIVATE METHODS

    void Awake () {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_scoreLabel = GameObject.Find("Score").GetComponent<Text>();
        m_startGameButton = GameObject.Find("StartGame").GetComponent<Button>();
        SetStateStartScreen();
    }
    
    public void SetStateStartScreen () {
        m_scoreLabel.gameObject.SetActive(false);
        m_startGameButton.gameObject.SetActive(true);
    }

    void SetStateGameScreen() {
        m_scoreLabel.gameObject.SetActive(true);
        m_startGameButton.gameObject.SetActive(false);
    }

    #endregion
}
