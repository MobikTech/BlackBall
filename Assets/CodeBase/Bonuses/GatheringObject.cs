using System;
using BlackBall.Core;
using BlackBall.Factories.Core;
using UnityEngine;

namespace BlackBall.Bonuses
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class GatheringObject : CoreBehaviour, IPoolItem<BonusCreationOptions>
    {
        public abstract string GetItemTypeKey { get; }
        
        private Action<GatheringObject> _despawnAction;

        public void SetupCreationOptions(BonusCreationOptions creationOptions)
        {
            transform.parent = creationOptions.Parent;
            transform.position = creationOptions.SpawnPoint;
            transform.rotation = creationOptions.Rotation;
            _despawnAction = creationOptions.Despawn;
        }

        protected abstract void HandleBonus();

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out BallController ballController))
            {
                _despawnAction.Invoke(this);
                HandleBonus();
            }
        }
    }
}