using System;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class Selection : BaseMode
    {
        private SelectionCursor _cursor;
        
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
            Debug.Log(Input.mousePosition);
        }
    }
}