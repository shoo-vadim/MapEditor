using System;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class Addition : BaseMode
    {
        // Один из варинтов спрятать TriggerAdd, это вывести его в событие
        // public event Action<Vector3, Shape> OnAdd;
        
        private Shape Shape { get; }
        
        private BaseCursor _cursor;

        private bool _isCursorShown;
        
        private bool _isMouseDown;
        
        public Addition(CursorManager cursorManager, Shape shape) : base(cursorManager) => Shape = shape;

        public override void OnSetup()
        {
            _cursor = CursorManager.Cursors
                    .FirstOrDefault(c => c is ShapeCursor shapeCursor && shapeCursor.Shape == Shape);
            
            if (_cursor == null) 
                throw new ArgumentException($"Unable to find cursor for shape {Shape}");
            
            _cursor.Show(true);
        }

        public override void OnDrop() => _cursor.Show(false);

        public override void OnUpdate(float deltaTime)
        {
            var ray = CursorManager.TargetCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, CursorManager.RaycastDistance, CursorManager.CursorMask))
            {
                if (!_isCursorShown)
                {
                    // Откешировал на всякий случай, но не уверен, что это нужно, возможно SetActive автоматом кеширует
                    _isCursorShown = true;
                    _cursor.gameObject.SetActive(true);
                }
                
                // Тут как-то нужно разруливать MouseDown отдельно для стейта 
                // Получается? У нас тут три стейта Off, Selection, Adding
                if (Input.GetMouseButtonDown(CursorManager.ControlMouseButton) && !_isMouseDown)
                {
                    _isMouseDown = true;
                    CursorManager.TriggerAdd(Shape, hit.point);
                }
                else if (Input.GetMouseButtonUp(CursorManager.ControlMouseButton) && _isMouseDown)
                {
                    _isMouseDown = false;
                }
                
                CursorManager.transform.position = hit.point;
            }
            else if (_isCursorShown)
            {
                _isCursorShown = false;
                _cursor.gameObject.SetActive(false);
            }
        }
    }
}