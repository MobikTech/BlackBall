using UnityEngine;

namespace BlackBall.Platforms.ConcretePlatforms
{
    public class PlatformSpiked : PlatformBase
    {
        public override string GetItemTypeKey => "Spiked";

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent(out BallController ballController))
            {
                ballController.TriggerDeath();
            }
        }
    }
}