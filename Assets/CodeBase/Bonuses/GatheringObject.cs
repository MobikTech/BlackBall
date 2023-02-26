using System;
using BlackBall.Core;
using BlackBall.Factories.Core;
using DG.Tweening;
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
                var lastScale = transform.localScale;
                transform.DOScale(0f, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    _despawnAction.Invoke(this);
                    transform.localScale = lastScale;
                });
                HandleBonus();
                ServiceLocator.ServiceLocatorInstance.SoundsPlayer.Play("PickUp");
            }
        }
    }
}