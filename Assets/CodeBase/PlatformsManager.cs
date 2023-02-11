using System.Collections.Generic;
using UnityEngine;

namespace BlackBall
{
    public class PlatformsManager : MonoBehaviour
    {
        private Platform[] _platforms;
        public List<Platform> ActivePlatforms { get; private set; }
        private void Awake()
        {
            _platforms = GetComponentsInChildren<Platform>();
        }
    }
}