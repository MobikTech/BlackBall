using UnityEngine;

namespace BlackBall.Core
{
    public class CoreBehaviour : MonoBehaviour
    {
        private string _cachedName;
        private bool _isNameCached;
        
        private GameObject _cachedGameObject;
        private bool _isGameObjectCached;
        
        private Transform _cachedTransform;
        private bool _isTransformCached;

        
        // PUBLIC MEMBERS
        public new string name
        {
            get
            {
#if UNITY_EDITOR
                if (Application.isPlaying == false)
                    return base.name;
#endif
                if (_isNameCached) 
                    return _cachedName;
                
                _cachedName = base.name;
                _isNameCached = true;

                return _cachedName;
            }
            set
            {
                if (string.CompareOrdinal(_cachedName, value) == 0) 
                    return;
                
                base.name = value;
                _cachedName = value;
                _isNameCached = true;
            }
        }

        public new GameObject gameObject
        {
            get
            {
#if UNITY_EDITOR
                if (Application.isPlaying == false)
                    return base.gameObject;
#endif

                if (_isGameObjectCached) 
                    return _cachedGameObject;
                
                _cachedGameObject = base.gameObject;
                _isGameObjectCached = true;
                
                return _cachedGameObject;
            }
        }

        public new Transform transform
        {
            get
            {
#if UNITY_EDITOR
                if (Application.isPlaying == false)
                    return base.transform;
#endif
                if (_isTransformCached) 
                    return _cachedTransform;
                
                _cachedTransform = base.transform;
                _isTransformCached = true;

                return _cachedTransform;
            }
        }

    }
}