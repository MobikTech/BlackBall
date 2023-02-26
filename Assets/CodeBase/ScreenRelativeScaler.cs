using UnityEngine;

namespace BlackBall
{
    public class ScreenRelativeScaler : MonoBehaviour
    {
        [SerializeField] private Vector2 _scale;
        
        private void Start()
        {
            float width = ScreenSize.GetScreenToWorldWidth();
            float height = ScreenSize.GetScreenToWorldHeight();
            transform.localScale = new Vector3(width * _scale.x, height * _scale.y, 1f);
        }
    }
}