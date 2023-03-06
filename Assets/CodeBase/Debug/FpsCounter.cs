using TMPro;
using UnityEngine;

namespace BlackBall.Debug
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fpsText = null!;

        private float _deltaTime;

        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            float fps = 1.0f / _deltaTime;
            _fpsText.text = "FPS: " + Mathf.RoundToInt(fps).ToString();
        }
    }
}