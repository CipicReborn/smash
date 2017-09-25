using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchManager : MonoBehaviour {

    #region PUBLIC MEMBERS

    public float MaxTapDurationInSeconds;
    public float MaxTapIntervalInSeconds;

    #endregion

    #region PUBLIC METHODS

    public void InitGame () {
        InitPlayers();
        currentTouches = null;
    }

    public void StopGame () {
        enabled = false;
    }

    public bool HasTouch (PlayerIds id) {
        return m_players[id].TouchFound;
    }

    public Vector2 GetTouchPosition (PlayerIds id) {
        return m_players[id].Touch.position;
    }

    public int GetTapCount (PlayerIds id) {
        return m_players[id].TapCount;
    }

    #endregion


    #region PRIVATE MEMBERS

    GameManager m_gameManager;
    Dictionary<PlayerIds, TouchPlayer> m_players;
    Touch[] currentTouches;

    #endregion


    #region PRIVATE METHODS

    void Awake() {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enabled = false;
    }

    void InitPlayers () {
        m_players = new Dictionary<PlayerIds, TouchPlayer>();

        foreach (Player lPlayer in m_gameManager.Players.Values) {
            if (lPlayer.Type == PlayerTypes.Human) {
                if (!enabled) {
                    enabled = true;
                }
                m_players[lPlayer.Id] = new TouchPlayer(lPlayer, MaxTapDurationInSeconds, MaxTapIntervalInSeconds);
                Debug.Log("Touch Player " + lPlayer.Id + " initialised.");
            }
        }
        Debug.Log("TouchManager : Players initialised");
    }

    void Update () {
        currentTouches = Input.touches;

        foreach (TouchPlayer player in m_players.Values) {
            player.InitFrame();
        }

        for (int i = 0; i < Input.touchCount; i++) {
            Touch currentTouch = currentTouches[i];

            foreach (TouchPlayer player in m_players.Values) {

                if (!player.TouchFound && player.IsInsideArea(currentTouch)) {
                    player.RecordTouch(currentTouch);
                }
            }

            bool allTouchPlayersDone = true;
            foreach (TouchPlayer player in m_players.Values) {
                if (!player.TouchFound) {
                    allTouchPlayersDone = false;
                }
            }

            if (allTouchPlayersDone) {
                break;
            }
        }
	}

    #endregion
    
    //const int m_MAX_TOUCHES = 4;
    //Color[] FeedbackColors = new Color[m_MAX_TOUCHES] { Color.cyan, Color.red, Color.yellow, Color.green };

    //void OnDrawGizmos() {
    //    for (int i = 0; i < Input.touchCount; i++) {
    //        drawTouchFeedback(i);
    //    }
    //}

    //void drawTouchFeedback (int i) {
    //    if (i >= m_MAX_TOUCHES) {
    //        Debug.Log("Impossible to feedback this touch <" + i.ToString() + "> because max Touches count is " + m_MAX_TOUCHES.ToString());
    //        return;
    //    }
    //    Gizmos.color = FeedbackColors[i];
    //    Gizmos.DrawSphere(convertScreenPosToGamePos(currentTouches[i].position), 1);
    //}

    //Vector3 convertScreenPosToGamePos (Vector2 pPosition) {
    //    Vector3 position = new Vector3(pPosition.x, pPosition.y, -0.5f);
    //    return Camera.main.ScreenToWorldPoint(position);
    //}
}
