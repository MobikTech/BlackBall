using UnityEngine;
using UnityEngine.UI;

namespace BlackBall
{
    [RequireComponent(typeof(Button))]
    public class ButtonSoundSubscriber : MonoBehaviour
    {
        [SerializeField] private string _soundName = null!;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(
                () => ServiceLocator.ServiceLocatorInstance.SoundsPlayer.Play(_soundName));
        }
    }
}