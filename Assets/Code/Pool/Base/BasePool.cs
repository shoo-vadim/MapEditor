using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    /*
     * Эта абстракция на случай, если нам понадобится пул для не-monobehaviour
     */
    public abstract class BasePool<TPoolable> : MonoBehaviour
        where TPoolable : IPoolable
    {
        protected readonly List<TPoolable> Poolable = new ();

        public abstract T Obtain<T>()
            where T : TPoolable;

        public virtual void Release(TPoolable poolable)
        {
            poolable.Drop();
            Poolable.Add(poolable);
        }
    }
}