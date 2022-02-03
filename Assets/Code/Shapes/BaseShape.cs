using UnityEngine;

namespace Code
{
    public abstract class BaseShape : MonoBehaviour, IPoolable
    {
        // Не сообразил, можно ли сделать через дженерики, поэтому ввел enum, чтобы сделать побысьрее 
        [SerializeField] 
        private Shape shape;

        public Shape Shape => shape;
        
        public abstract void Drop();
    }
}