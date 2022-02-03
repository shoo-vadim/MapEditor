using UnityEngine;

namespace Code
{
    public abstract class PrefabedItem : MonoBehaviour, IPoolItem
    {
        public abstract void Setup();
        public abstract void Drop();
    }
}