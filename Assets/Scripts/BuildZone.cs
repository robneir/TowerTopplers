using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class BuildZone : MonoBehaviour {
    
    private TeamFilter m_TeamFilter;
    private SpriteRenderer m_SpriteRenderer;

	// Use this for initialization
	void Start () {
        m_TeamFilter = this.GetComponent<TeamFilter>();
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
		if(m_TeamFilter.team == TeamFilter.Team.Blue)
        {
            m_SpriteRenderer.color = Constants.Instance.BlueTeamZoneColor;
        }
        else
        {
            m_SpriteRenderer.color = Constants.Instance.RedTeamZoneColor;
        }
	}
}
