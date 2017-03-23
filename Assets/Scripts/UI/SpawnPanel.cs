using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerControl;

namespace UI
{
    public class SpawnPanel : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Button prefab for spawnables to be represented in UI.")]
        private Button SpawnButtonPrefab;

        private List<Spawnable> m_SpawnablesList;

        public void PopulateSpawnables(SpawningCapability _spawningCability)
        {
            m_SpawnablesList = _spawningCability.SpawnablesList;
            if (SpawnButtonPrefab != null)
            {
                for (int i = 0; i < m_SpawnablesList.Count; i++)
                {
                    Button spawnButtonInstance = Instantiate(SpawnButtonPrefab, this.transform);
                    Spawnable spawnObject = m_SpawnablesList[i];
                    spawnButtonInstance.onClick.AddListener(delegate { _spawningCability.Spawn(spawnObject); });
                    Text buttonText = spawnButtonInstance.GetComponentInChildren<Text>();
                    buttonText.text = m_SpawnablesList[i].SpawnName;
                    spawnButtonInstance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
            }
        }
    }
}
