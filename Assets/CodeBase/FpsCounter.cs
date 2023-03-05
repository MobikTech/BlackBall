using TMPro;
using UnityEngine;

namespace BlackBall
{
    // public class FpsCounter : MonoBehaviour
    // {
    //     [SerializeField] private TMP_Text _fpsText = null!;
    //     [SerializeField] private float _hudRefreshRate = 0.1f;
    //
    //     private float _timer;
    //
    //     private void Update()
    //     {
    //         if (Time.unscaledTime > _timer)
    //         {
    //             int fps = (int)(1f / Time.unscaledDeltaTime);
    //             _fpsText.text = "FPS: " + fps;
    //             _timer = Time.unscaledTime + _hudRefreshRate;
    //         }
    //     }
    // }

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