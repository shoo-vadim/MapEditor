using Code.State;
using UnityEngine;

namespace Code
{
    public abstract class MonoShape : MonoPoolable, IStateful<ShapeProps>
    {
        public ShapeProps Props
        {
            get => _currentProps;
            set
            {
                renderer.transform.localScale = value.Scale;
                /*
                 * Лучше использовать MaterialPropertyBlock, но не критично.
                 * Так как тот же SRP Batcher не будет с ним работать
                 */
                renderer.material.SetColor(ColorProp, value.Color);
                _currentProps = value;
            }
        }

        private static readonly int ColorProp = Shader.PropertyToID("_Color");

        public Renderer Renderer => renderer;

        private ShapeProps _currentProps;

        [SerializeField] 
        private new Renderer renderer;
        
        private void OnValidate()
        {
            if (renderer == null)
                renderer = Components.Find<Renderer>(gameObject);
        }
    }
}