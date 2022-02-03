using UnityEngine;

namespace Code
{
    public abstract class BaseShape : MonoBehaviour, IPoolable
    {
        public abstract void Drop();
    }
}