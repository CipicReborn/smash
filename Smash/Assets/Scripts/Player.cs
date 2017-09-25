using UnityEngine;

public class Player {

    #region PUBLIC PROPERTIES

    public PlayerIds Id {
        get {
            return m_id;
        }
    }

    public PlayerTypes Type {
        get {
            return m_type;
        }
    }

    public AIs AI {
        get {
            return m_AI;
        }
    }

    public int CurrentScore {
        get {
            return m_currentScore;
        }
    }

    #endregion

    #region PUBLIC METHODS

    public Player(PlayerIds pId, PlayerTypes pType, string pPadName, AIs pAI = AIs.Easy) {
        m_id = pId;
        m_type = pType;
        m_padName = pPadName;
        m_AI = pAI;
        InitPad();
    }

    public void Stop () {
        m_pad.GetComponent<IController>().Stop();
    }

    public void ResetScore () {
        m_currentScore = 0;
    }

    public void Score1Point () {
        m_currentScore++;
    }
    #endregion


    #region PRIVATE MEMBERS

    PlayerIds m_id;
    PlayerTypes m_type;
    AIs m_AI;
    int m_currentScore = 0;
    string m_padName;
    GameObject m_pad;

    #endregion


    #region PRIVATE METHODS

    void InitPad() {

        m_pad = GameObject.Find(m_padName);

        if (m_type == PlayerTypes.Human) {
            m_pad.AddComponent<PlayerController>().Init(m_id);
        }
        else if (m_type == PlayerTypes.CPU) {
            m_pad.AddComponent<IAController>().Init(m_id);
        }
    }

    #endregion
}
