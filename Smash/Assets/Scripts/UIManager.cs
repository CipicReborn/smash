using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    #region PUBLIC SCREEN ANIMATORS

    public Animator StartScreen;
    public Animator ModeSelectionScreen;
    public Animator RoundsSelectionScreen;
    public Animator DifficultySelectionScreen;
    public Animator GameScreen;

    #endregion

    #region PUBLIC BUTTON CALLBACKS

    public void SelectSoloGameType () {
        m_gameManager.GameType = GameTypes.Solo;
        OpenScreen(ModeSelectionScreen);
    }

    public void SelectDuoGameType () {
        m_gameManager.GameType = GameTypes.Duo;
        OpenScreen(RoundsSelectionScreen);
    }

    public void SelectDebugGameType() {
        m_gameManager.GameType = GameTypes.Debug;
        StartGame(1);
    }

    public void SelectEasyAI () {
        m_gameManager.AI = AIs.Easy;
        OpenScreen(RoundsSelectionScreen);
    }

    public void SelectMediumAI () {
        m_gameManager.AI = AIs.Medium;
        OpenScreen(RoundsSelectionScreen);
    }

    public void SelectHardAI () {
        m_gameManager.AI = AIs.Hard;
        OpenScreen(RoundsSelectionScreen);
    }


    public void SelectSimpleMode () {
        m_gameManager.SoloMode = SoloModes.Simple;
        OpenScreen(DifficultySelectionScreen);
    }

    public void SelectCareerMode () {
        m_gameManager.SoloMode = SoloModes.Career;
    }

    public void SelectSurvivalMode () {
        m_gameManager.SoloMode = SoloModes.Survival;
    }


    public void SetCareerName(string name) {
        m_gameManager.CareerName = name;
    }

    public void StartGame(int roundsCount) {
        OpenScreen(GameScreen);
        SetGameSettings(roundsCount);
        m_gameManager.StartGame();
    }

    #endregion

    #region PUBLIC METHODS

    public void OpenStartScreen () {
        OpenScreen(StartScreen);
    }

    public void OpenScreen (Animator screenAnimator) {
        if (m_currentScreen == screenAnimator) {
            return;
        }
        screenAnimator.gameObject.SetActive(true);
        screenAnimator.transform.SetAsLastSibling();
        CloseCurrentScreen();
        m_currentScreen = screenAnimator;
        m_currentScreen.SetBool(m_openScreenParameterId, true);
    }

    public void CloseCurrentScreen () {
        if (m_currentScreen == null) {
            return;
        }
        m_currentScreen.SetBool(m_openScreenParameterId, false);
        StartCoroutine(DisableScreenDelayed(m_currentScreen));
        m_currentScreen = null;
    }

    public IEnumerator StartEngagementCountdown (int value, Action callback = null) {

        m_timerText.gameObject.SetActive(true);
        m_timerText.GetComponent<Text>().text = value.ToString();
        m_timerText.localScale = Vector3.one;

        for (int i = 0; i < 51; i++) {
            float scale = 1.0f - i / 50.0f;
            m_timerText.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        if (value > 1) {
            yield return StartCoroutine(StartEngagementCountdown(value - 1));
        }

        if (callback != null) {
            m_timerText.gameObject.SetActive(false);
            callback();
        }
    }
    
    public IEnumerator DisplayBoom () {
        m_boomFeedback.localScale = Vector3.zero;
        m_boomFeedback.gameObject.SetActive(true);
        Camera.main.GetComponent<ScreenShake>().ShakeCameraForSeconds(0.5f, 0.4f);
        GetComponent<AudioSource>().Play();
        float tweenDuration = 5.0f;
        float displayDuration = 30.0f;
        for (int i = 0; i < tweenDuration + 1; i++) {
            float scale = 0.0f + i / tweenDuration;
            m_boomFeedback.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        for (int i = 0; i < displayDuration + 1; i++) {
            yield return null;
        }
        m_boomFeedback.gameObject.SetActive(false);
    }

    public void UpdateScore(int p1Score, int p2Score) {
        m_scoreLabel.text = p1Score.ToString("D2") + " - " + p2Score.ToString("D2");
    }

    #endregion


    #region PRIVATE MEMBERS

    GameManager m_gameManager;
    Text m_scoreLabel;
    Transform m_timerText;
    Transform m_boomFeedback;

    Animator m_currentScreen;
    int m_openScreenParameterId;
    const string m_OPEN_SCREEN_PARAMETER_NAME = "isOpen";
    const string m_CLOSED_STATE_NAME = "Closed";

    #endregion


    #region PRIVATE METHODS

    void Awake () {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_scoreLabel = GameObject.Find("Score").GetComponent<Text>();
        m_timerText = GameScreen.transform.Find("TimerText");
        m_timerText.gameObject.SetActive(false);
        m_boomFeedback = GameScreen.transform.Find("BoomContainer");
        m_boomFeedback.gameObject.SetActive(false);
        GameScreen.gameObject.SetActive(false);

        m_openScreenParameterId = Animator.StringToHash(m_OPEN_SCREEN_PARAMETER_NAME);
        
    }

    void SetGameSettings(int roundsCount, int roundsLength = 10) {
        m_gameManager.RoundsCount = roundsCount;
        m_gameManager.RoundsLength = roundsLength;
    }

    void Start () {
        if (StartScreen == null)
            return;
        OpenScreen(StartScreen);
    }

    IEnumerator DisableScreenDelayed (Animator screenAnimator) {
        bool closedStateReached = false;
        bool wantToClose = true;
        while (!closedStateReached && wantToClose) {
            if (!screenAnimator.IsInTransition(0))
                closedStateReached = screenAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_CLOSED_STATE_NAME);

            wantToClose = !screenAnimator.GetBool(m_openScreenParameterId);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
            screenAnimator.gameObject.SetActive(false);
    }

    #endregion
}
