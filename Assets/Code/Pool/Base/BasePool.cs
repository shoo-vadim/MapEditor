using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    /*
     * Переписал пул, немного упростив его. Теперь объект
     * запрашивается через класс, как я изначально и хотел
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