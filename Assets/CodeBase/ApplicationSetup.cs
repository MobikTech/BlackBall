using UnityEngine;

namespace BlackBall
{
    public class ApplicationSetup : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 90;
        
        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}