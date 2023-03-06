using BlackBall.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace BlackBall.UI.Widgets
{
    [RequireComponent(typeof(Button))]
    public class SoundSubscriberWidget : UIWidget
    {
        [SerializeField] private string _soundName = null!;

        internal override void Initialize()
        {
            base.Initialize();
            GetComponent<Button>().onClick.AddListener(
                () => ServiceLocator.ServiceLocatorInstance.SoundsPlayer.Play(_soundName));
        }
    }
}