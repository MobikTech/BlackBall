using Mobik.Common.Utilities.UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace BlackBall.UI.Widgets
{
    [RequireComponent(typeof(Button))]
    public class SoundSubscriberWidget : UIWidget
    {
        [SerializeField] private string _soundName = null!;

        public override void Initialize()
        {
            GetComponent<Button>().onClick.AddListener(() => ServiceLocator.ServiceLocatorInstance.SoundsPlayer.Play(_soundName));
        }
    }
}