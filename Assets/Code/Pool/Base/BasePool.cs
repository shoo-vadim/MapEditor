using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code
{
    /*
     * Отмечу, что с пулом объектов не работал никогда, так что не могу предусмотреть подводные камни.
     * Еще, т.к. изначально планировал работать напрямую с классами IPoolable, но в итоге перешел на enum
     * Решил оставить базовый дженерик-класс, TRequest это enum, который мэппится к нужному 
     */
    public abstract class BasePool<TRequest, TItem> : MonoBehaviour 
        where TItem : IPoolItem
    {
        protected readonly List<TItem> Items = new List<TItem>();
        
        // Т.к. теперь нам нужно мэппать TRequest -> TItem, мы оставляем функцию абстрактной
        public abstract TItem Obtain(TRequest request);
        
        // protected bool TryObtain<T>(out TItem item) 
        //     where T : TItem
        // {
        //     // TODO: Нужно будет перепроверить выдасться ли базовый тип при таком сравнении
        //     item = Items.FirstOrDefault(i => i.GetType() == typeof(T));
        //     return item != null;
        // }

        // protected TItem Obtain(Type type)
        // {
        //     var item = Items.FirstOrDefault(i => i.GetType() == type);
        //     // if (item == null) t
        // }
        
        // protected bool TryObtain(Type type, out TItem item)
        // {
        //     item = Items.FirstOrDefault(i => i.GetType() == type);
        //     return item != null;
        // }

        public void Release(TItem item)
        {
            item.Drop();
            Items.Add(item);
        }
    }
}