using Code.State;
using UnityEngine;

namespace Code
{
    public abstract class MonoShape : MonoPoolable, ISelectable, IStateful<ShapeProps>
    {
        private static readonly int ColorProp = Shader.PropertyToID("_Color");
        
        /*
         * Этот стейт нужен, чтобы потом можно было легко обрабатывать undo/redo, сохранять и т.д.
         */
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
        
        private ShapeProps _currentProps;

        [SerializeField] 
        private new Renderer renderer;
        
        [SerializeField] 
        private Transform holder;
        
        public Transform Anchor => holder;

        public Bounds Bounds => renderer.bounds;

        private void OnValidate()
        {
            if (renderer == null)
                renderer = this.Detect<Renderer>();
        }
    }
}