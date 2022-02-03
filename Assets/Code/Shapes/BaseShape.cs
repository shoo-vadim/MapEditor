using UnityEngine;

namespace Code
{
    public abstract class BaseShape : PrefabedItem
    {
        
        
        // Не сообразил, можно ли сделать через дженерики/типы, поэтому ввел enum на всякий случай
        [SerializeField] 
        private Shape shape;

        public Shape Shape => shape;

        // Вынесено из самого пула, т.к. в пул мы в дальнейшем можем класть не-монобехи 
        public override void Setup()
        {
            gameObject.SetActive(true);
        }

        public override void Drop()
        {
            gameObject.SetActive(false);
        }
    }
}