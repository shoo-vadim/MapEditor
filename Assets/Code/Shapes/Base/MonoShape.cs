using Code.State;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code
{
    public abstract class MonoShape : PrefabedItem, 
        IStateful<ShapeState>,
        IPointerClickHandler
    {
        public ShapeState State
        {
            get => _currentState;
            set
            {
                renderer.transform.localScale = value.Scale;
                // Лучше использовать MaterialPropertyBlock, но не критично. Так как тот же SRP Batcher не будет с ним работать
                renderer.material.SetColor("_Color", value.Color);
                _currentState = value;
            }
        }

        public Shape Shape => shape;
        
        // [SerializeField] 
        private ShapeState _currentState;
        
        // Не сообразил, можно ли сделать через дженерики/типы, поэтому ввел enum на всякий случай
        [SerializeField] 
        private Shape shape;

        [SerializeField] 
        private new Renderer renderer;

        // Вынесено из самого пула, т.к. в пул мы в дальнейшем можем создавать пулы с не-монобехами
        public override void Setup()
        {
            gameObject.SetActive(true);
        }

        public override void Drop()
        {
            gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"Clicked: {name}");
        }

        private void OnValidate()
        {
            if (renderer == null)
                renderer = Components.Find<Renderer>(gameObject);
        }
    }
}