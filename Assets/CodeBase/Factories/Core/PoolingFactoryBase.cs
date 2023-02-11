using System.Collections.Generic;
using BlackBall.Core;
using UnityEngine;

namespace BlackBall.Factories.Core
{
    public abstract class PoolingFactoryBase<TItemBase, TCreationOptions> : IPoolingFactory<TItemBase, TCreationOptions> 
        where TItemBase : MonoBehaviour, IPoolItem<TCreationOptions>
        where TCreationOptions : IOptions
    {
        protected PoolingFactoryBase()
        {
            _poolOfItems = new Dictionary<string, Stack<TItemBase>>();
        }

        protected virtual int InitialStackCapacity => 300;
        private readonly Dictionary<string, Stack<TItemBase>> _poolOfItems;

        public virtual void Delete(TItemBase unit)
        {
            if (!_poolOfItems.ContainsKey(unit.GetItemTypeKey))
                _poolOfItems.Add(unit.GetItemTypeKey, new Stack<TItemBase>(InitialStackCapacity));
            
            _poolOfItems[unit.GetItemTypeKey].Push(unit);
            unit.gameObject.SetActive(false);
        }

        public virtual TItemBase Create(TItemBase prefab, TCreationOptions creationOptions)
        {
            if (!_poolOfItems.ContainsKey(prefab.GetItemTypeKey))
                _poolOfItems.Add(prefab.GetItemTypeKey, new Stack<TItemBase>(InitialStackCapacity));

            TItemBase unit = _poolOfItems[prefab.GetItemTypeKey].Count == 0 
                ? Object.Instantiate(prefab)
                : _poolOfItems[prefab.GetItemTypeKey].Pop();

            unit.SetupCreationOptions(creationOptions);
            unit.gameObject.SetActive(true);
            return unit;
        }
    }
}