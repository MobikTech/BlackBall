using UnityEngine;

namespace BlackBall
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DeathTrigger : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out BallController ballController))
            {
                ServiceLocator.ServiceLocatorInstance.PerGameData.Pause.IsPaused = true;
                ballController.TriggerDeath();
            }
        }
    }
}