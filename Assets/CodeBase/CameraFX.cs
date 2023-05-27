using UnityEngine;

namespace BlackBall
{
    [ExecuteInEditMode, RequireComponent(typeof(Camera))]
    public class CameraFX : MonoBehaviour
    {
        [SerializeField] private Material? _FXMaterial;
        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (_FXMaterial == null)
                Graphics.Blit(src, dest);
            else
                Graphics.Blit(src, dest, _FXMaterial);
        }
    }
}