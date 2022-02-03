using System.Collections.Generic;
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

        public void Release(TItem item)
        {
            item.Drop();
            Items.Add(item);
        }
    }
}