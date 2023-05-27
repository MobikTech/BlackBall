using System;
using DG.Tweening;
using Mobik.Common.Core;
using Mobik.Common.Utilities.PoolingFactory.Abstr;
using UnityEngine;

namespace BlackBall.Bonuses
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class GatheringObject : MonoBehaviourCached, IPoolItem
    {
        public event Action<GatheringObject> ObjectDespawned;
        public abstract string GetItemTypeKey { get; }

        protected abstract void HandleBonus();

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out BallController ballController))
            {
                var lastScale = transform.localScale;
                transform.DOScale(0f, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    ObjectDespawned.Invoke(this);
                    transform.localScale = lastScale;
                });
                HandleBonus();
                ServiceLocator.ServiceLocatorInstance.SoundsPlayer.Play("PickUp");
            }
        }
    }
}