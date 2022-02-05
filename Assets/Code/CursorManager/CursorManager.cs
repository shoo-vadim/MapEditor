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

        private BaseMode _currentMode;

        public void Use(BaseMode mode)
        {
            if (mode != _currentMode)
            {
                _currentMode?.OnDrop();
            }

            _currentMode = mode;
            _currentMode.OnSetup();
        }
        
        /*
         * Не очень элегантно получается, мы вызываем из mode-объекта функцию триггера,
         * но при этом она доступна и для самого App. Не думал пока, как можно спрятать
         */
        public void TriggerAdd(Shape shape, Vector3 position) => 
            OnShapeAdded?.Invoke(shape, position);

        public void TriggerSelectionBox(Bounds bounds) => 
            OnSelectionBox?.Invoke(bounds);

        // Всегда прописываю Unity-messages как protected virtual, чтобы ненароком не перезаписать их при наследовании
        protected virtual void Update()
        {
            _currentMode?.OnUpdate(Time.deltaTime);
            // if (_currentCursorMode != CursorModeKind.Off) DrawCursor();
        }
    }
}