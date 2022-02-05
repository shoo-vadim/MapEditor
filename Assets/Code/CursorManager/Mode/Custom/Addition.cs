using System;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class Addition : BaseMode
    {
        /*
         * Один из варинтов спрятать TriggerAdd, это вывести его в событие,
         * подписываясь на него, при переходе в Addition-режим в CursorManager
         */
        // public event Action<Vector3, Shape> OnAdd;
        
        private Shape Shape { get; }
        
        private BaseCursor _cursor;

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
                _cursor.Show(true);
                
                if (CursorManager.IsMouseDown && !_isMouseDown)
                {
                    _isMouseDown = true;
                    CursorManager.TriggerAdd(Shape, hit.point);
                }
                else if (CursorManager.IsMouseUp && _isMouseDown)
                {
                    _isMouseDown = false;
                }
                
                CursorManager.transform.position = hit.point;
            }
            else
            {
                _cursor.Show(false);
            }
        }
    }
}