using System;
using UnityEngine;

namespace Code
{
    public class CursorManager : MonoBehaviour
    {
        public event Action<Bounds> OnSelectionBox;
        public event Action<Shape, Vector3> OnShapeAdded;
        
        public const float RaycastDistance = 1000f;
        
        public const int ControlMouseButton = 0;

        public SelectionBox SelectionBox => selectionBox;

        public BaseCursor[] Cursors => cursors;

        public Camera TargetCamera => targetCamera;

        public LayerMask CursorMask => cursorMask;
        
        /*
         * Заметил, что в разных режимах этот функционал переиспользуется,
         * Плюс, так можно вывести работу со статическим Input,
         * чтобы в дальнейшем было проще написать заглушку для тестирования  
         */
        public bool IsMouseDown => Input.GetMouseButtonDown(ControlMouseButton);
        
        public bool IsMouseUp => Input.GetMouseButtonUp(ControlMouseButton);
        
        public Vector3 MousePosition => Input.mousePosition;
        
        [SerializeField]
        private Camera targetCamera;
        
        [SerializeField] 
        private LayerMask cursorMask;
        
        [SerializeField] 
        private SelectionBox selectionBox;

        [SerializeField] 
        private BaseCursor[] cursors;

        private IMode _currentMode;
        
        /*
         * Это получается не совсем лаконично, но зато типобезопасно
         * Можно попробовать разрулить так
         * https://stackoverflow.com/questions/53559927/is-it-possible-to-remove-a-generic-parameter-of-this-method
         */
        public void Use<TMode, TSettings>(TSettings settings)
            where TMode : BaseMode<TSettings>, new()
            where TSettings: ModeSettings
        {
            // Тут можно дополнительно делать сравнение по ранее выставленным TMode и TSettings
            if (_currentMode != null)
            {
                _currentMode.OnDrop();
                UnsubscribeMode(_currentMode);
            }

            _currentMode = new TMode
            {
                Settings = settings,
                CursorManager = this
            };
            
            SubscribeMode(_currentMode);
            _currentMode.OnSetup();
        }

        /*
         * Не очень красиво все прячется под капотом, но зато
         * во внешнее API менеджера не выносятся внутренние
         * методы TriggerAdd, TriggerSelectionBox и т.д.
         */
        private void SubscribeMode(IMode mode)
        {
            switch (mode)
            {
                case Addition addition:
                    addition.OnAdd += TriggerAdd;
                    break;
                case Selection selection:
                    selection.OnSelectionBox += TriggerSelectionBox;
                    break;
            }
        }
        
        private void UnsubscribeMode(IMode mode)
        {
            switch (mode)
            {
                case Addition addition:
                    addition.OnAdd -= TriggerAdd;
                    break;
                case Selection selection:
                    selection.OnSelectionBox -= TriggerSelectionBox;
                    break;
            }
        }
        
        private void TriggerAdd(Shape shape, Vector3 position) => 
            OnShapeAdded?.Invoke(shape, position);

        private void TriggerSelectionBox(Bounds bounds) => 
            OnSelectionBox?.Invoke(bounds);

        // Всегда прописываю Unity-messages как protected virtual, чтобы ненароком не перезаписать их при наследовании
        protected virtual void Update()
        {
            _currentMode?.OnUpdate(Time.deltaTime);
        }
    }
}