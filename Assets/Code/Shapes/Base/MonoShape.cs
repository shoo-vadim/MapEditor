using Code.State;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code
{
    public abstract class MonoShape : PrefabedItem, IStateful<ShapeProps>
    {
        public ShapeProps Props
        {
            get => _currentProps;
            set
            {
                renderer.transform.localScale = value.Scale;
                // Лучше использовать MaterialPropertyBlock, но не критично. Так как тот же SRP Batcher не будет с ним работать
                renderer.material.SetColor(ColorProp, value.Color);
                _currentProps = value;
            }
        }
        
        private static readonly int ColorProp = Shader.PropertyToID("_Color");

        public Renderer Renderer => renderer;

        private ShapeProps _currentProps;

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

        // public void OnPointerClick(PointerEventData eventData)
        // {
        //     Debug.Log($"Clicked: {name}");
        // }

        private void OnValidate()
        {
            if (renderer == null)
                renderer = Components.Find<Renderer>(gameObject);
        }
    }
}