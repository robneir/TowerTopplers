using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Utility
{
    public class GameMaster : MonoBehaviour
    {

        public static GameMaster Instance;

        public Dictionary<NetworkIdentity, Player> mPlayersMap;

        void Awake()
        {
            if (Instance != null)
            {
                GameObject.Destroy(this);
            }
            else
            {
                Instance = this;
            }

            DontDestroyOnLoad(this);
        }
    }
}