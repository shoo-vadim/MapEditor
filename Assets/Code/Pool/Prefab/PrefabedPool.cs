using System;
using System.Linq;
using UnityEngine;

namespace Code
{
    public abstract class PrefabedPool<TRequest, TItem> : BasePool<TRequest, TItem> 
        where TItem : PrefabedItem
    {
        [SerializeField] 
        private TItem[] prefabs;

        protected TItem ObtainInstance(Type type)
        {
            var item = Items.FirstOrDefault(i => i.GetType() == type);
            if (item == null)
            {
                var prefab = prefabs.FirstOrDefault(p => p.GetType() == type);
                if (prefab == null) throw new ArgumentException($"Unable to find prefab of type {type}");
                item = Instantiate(prefab);
            }
            item.Setup();
            return item;
        }
    }
}