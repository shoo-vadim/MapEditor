using System;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class Selection : BaseMode
    {
        /*
         * Тут курсор не особенно полезен, но пусть пока будет
         * т.к. не предусмотрел изначально, что будет мультиселект,
         * Можно для красоты изменить на 2d-шный
         */
        private SelectionCursor _cursor;

        private Vector3 _mouseDown;
        
        public Selection(CursorManager cursorManager) : base(cursorManager) { }

        public override void OnSetup()
        {
            if (_cursor == null)
            {
                _cursor = CursorManager.Cursors
                    .FirstOrDefault(c => c is SelectionCursor) as SelectionCursor;
            
                if (_cursor == null) 
                    throw new ArgumentException($"Unable to find selection cursor");    
            }
            
            _cursor.Show(true);
        }

        public override void OnDrop()
        {
            _cursor.Show(false);
        }

        public override void OnUpdate(float deltaTime)
        {
            // if ( CursorManager.ControlMouseButton
            Debug.Log(Input.mousePosition);
        }
    }
}