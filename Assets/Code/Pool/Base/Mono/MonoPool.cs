using System;
using System.Linq;
using UnityEngine;

namespace Code
{
    public abstract class MonoPool<TMonoPoolable> : BasePool<TMonoPoolable>
        where TMonoPoolable : MonoPoolable
    {
        [SerializeField] 
        private Transform holder;
        
        [SerializeField] 
        private TMonoPoolable[] prefabs;

        protected TMonoPoolable[] Prefabs => prefabs;

        public override T Obtain<T>()
        {
            var monoPoolable = Poolable.FirstOrDefault(p => p.GetType() == typeof(T));
            if (monoPoolable == null)
            {
                var prefab = prefabs
                    .FirstOrDefault(p => p.GetType() == typeof(T));
        
                if (prefab == null)
                    throw new ArgumentException($"Unable to find prefab of type {typeof(T)}");
                
                monoPoolable = Instantiate(prefab, holder);
            }
            
            monoPoolable.Show(true);

            /*
             * По-идее, тут должно конвертиться без проблем, но компилятор
             * почему-то не видит неявного преобразования. Не могу сообразить, в чем тут дело.
             * Можно сделать как Obtain(T type), но тогда не будет проверяться на этапе компиляции
             */
            return (T)monoPoolable;
        }

        public override void Release(TMonoPoolable poolable)
        {
            poolable.Show(false);
            base.Release(poolable);
        }
    }
}