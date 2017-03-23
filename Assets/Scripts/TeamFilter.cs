using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamFilter : NetworkBehaviour {

    public enum Team
    {
        Blue,
        Red
    }

    [SerializeField]
    [Tooltip("What team I am on.")]
    private Team m_Team;
    public Team team
    {
        get
        {
            return m_Team;
        }
        set
        {
            m_Team = value;
        }
    }
}
