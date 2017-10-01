using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    #region PUBLIC MEMBERS

    public Animator StartScreen;
    public Animator ModeSelectionScreen;
    public Animator RoundsSelectionScreen;
    public Animator DifficultySelectionScreen;
    public Animator GameScreen;

    #endregion


    #region PUBLIC METHODS

    public void UpdateScore (int p1Score, int p2Score) {
        m_scoreLabel.text = p1Score.ToString("D2") + " - " + p2Score.ToString("D2");
    }

    public void SelectSoloGameType () {
        m_gameManager.GameType = GameTypes.Solo;
        OpenScreen(ModeSelectionScreen);
    }

    public void SelectDuoGameType () {
        m_gameManager.GameType = GameTypes.Duo;
        OpenScreen(RoundsSelectionScreen);
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

    public void SetGameSettings (int roundsCount, int roundsLength = 10) {
        m_gameManager.RoundsCount = roundsCount;
        m_gameManager.RoundsLength = roundsLength;
    }

    public void SetCareerName (string name) {
        m_gameManager.CareerName = name;
    }

    public void StartGame (int roundsCount) {
        OpenScreen(GameScreen);
        SetGameSettings(roundsCount);
        m_gameManager.StartGame();
    }

    public void OpenStartScreen () {
        OpenScreen(StartScreen);
    }

    public void OpenScreen(Animator screenAnimator) {
        if (m_currentScreen == screenAnimator) {
            return;
        }

        screenAnimator.gameObject.SetActive(true);
        //Save the currently selected button that was used to open this Screen. (CloseCurrent will modify it)
        //var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;
        //Move the Screen to front.
        screenAnimator.transform.SetAsLastSibling();

        CloseCurrentScreen();

        //m_PreviouslySelected = newPreviouslySelected;

        m_currentScreen = screenAnimator;
        m_currentScreen.SetBool(m_openScreenParameterId, true);

        ////Set an element in the new screen as the new Selected one.
        //GameObject go = FindFirstEnabledSelectable(anim.gameObject);
        //SetSelected(go);
    }

    public void CloseCurrentScreen() {
        if (m_currentScreen == null) {
            return;
        }
        m_currentScreen.SetBool(m_openScreenParameterId, false);

        //Reverting selection to the Selectable used before opening the current screen.
        //SetSelected(m_PreviouslySelected);
        StartCoroutine(DisableScreenDelayed(m_currentScreen));
        m_currentScreen = null;
    }

    #endregion


    #region PRIVATE MEMBERS

    GameManager m_gameManager;
    Text m_scoreLabel;
    Button m_startGameButton;

    Animator m_currentScreen;
    int m_openScreenParameterId;
    const string m_OPEN_SCREEN_PARAMETER_NAME = "isOpen";
    const string m_CLOSED_STATE_NAME = "Closed";

    #endregion


    #region PRIVATE METHODS

    void Awake () {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_scoreLabel = GameObject.Find("Score").GetComponent<Text>();
        GameScreen.gameObject.SetActive(false);

        m_openScreenParameterId = Animator.StringToHash(m_OPEN_SCREEN_PARAMETER_NAME);

    }

    void Start() {
        if (StartScreen == null)
            return;
        OpenScreen(StartScreen);
    }


    IEnumerator DisableScreenDelayed(Animator screenAnimator) {
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
