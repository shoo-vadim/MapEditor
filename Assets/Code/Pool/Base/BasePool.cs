using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    // Отмечу, что с пулом объектов не работал никогда,
    public abstract class BasePool<TPoolable> : MonoBehaviour where TPoolable : IPoolable
    {
        protected readonly List<TPoolable> Items = new List<TPoolable>();

        public virtual TItem Obtain<TItem>() 
            where TItem : TPoolable, new()
        {
            var item = new TItem();
            return item;
        }

        public virtual void Release(TPoolable item)
        {
            item.Drop();
            Items.Add(item);
        }
    }
}