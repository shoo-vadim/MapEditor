using UnityEngine;

namespace Code
{
    public abstract class MonoPoolable : MonoBehaviour, IPoolable
    {
        public virtual void Setup() {}
        public virtual void Drop() {}
    }
}