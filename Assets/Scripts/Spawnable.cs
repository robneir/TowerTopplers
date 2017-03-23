using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utility;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(BoxCollider))]
public class Spawnable : NetworkBehaviour {

    [Tooltip("Name that spawnable will be listed as in UI and refered to visually.")]
    public string SpawnName;

    [SerializeField]
    [Tooltip("Indicator for valid build location.")]
    private SpriteRenderer BuildSpriteRenderer;

    private bool m_IsInValidBuildArea;  // Changes depending on if the spawnable is in a valid build location.

    // Use this for initialization
    void Start (){
        BuildSpriteRenderer.color = Constants.Instance.failBuildColor;
    }

    public bool IsInValidBuildArea()
    {
        return m_IsInValidBuildArea;
    }

    void OnTriggerEnter(Collider col)
    {
        BuildZone buildZoneComp = col.gameObject.GetComponent<BuildZone>();
        if (buildZoneComp != null)
        {
            BuildSpriteRenderer.color = Constants.Instance.canBuildColor;
            m_IsInValidBuildArea = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        BuildZone buildZoneComp = col.gameObject.GetComponent<BuildZone>();
        if(buildZoneComp != null)
        {
            BuildSpriteRenderer.color = Constants.Instance.failBuildColor;
            m_IsInValidBuildArea = false;
        }
    }
}
