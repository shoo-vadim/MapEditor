using System;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class Addition : BaseMode<AdditionSettings>
    {
        /*
         * Один из варинтов спрятать TriggerAdd, это вывести его в событие,
         * подписываясь на него, при переходе в Addition-режим в CursorManager
         */
        public event Action<Shape, Vector3> OnAdd;

        private BaseCursor _cursor;

        private bool _isMouseDown;

        public override void OnSetup()
        {
            _cursor = CursorManager.Cursors
                    .FirstOrDefault(c => c is ShapeCursor shapeCursor && shapeCursor.Shape == Settings.Shape);
            
            if (_cursor == null) 
                throw new ArgumentException($"Unable to find cursor for shape {Settings.Shape}");
            
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
                    OnAdd?.Invoke(Settings.Shape, hit.point);
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