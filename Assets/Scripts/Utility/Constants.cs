using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Constants : MonoBehaviour
    {
        public static Constants Instance;

        [Tooltip("Color of sprite around spawnable if it can be built in its current location.")]
        public Color canBuildColor;
        [Tooltip("Color of sprite around spawnable if it cannot be built in its current location.")]
        public Color failBuildColor;

        [Tooltip("It's the damn blue team's color.")]
        public Color BlueTeamColor;
        [Tooltip("It's the damn red team's color.")]
        public Color RedTeamColor;

        [Tooltip("It's the blue team's build zone color.")]
        public Color BlueTeamZoneColor;
        [Tooltip("It's the red team's build zone color.")]
        public Color RedTeamZoneColor;

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