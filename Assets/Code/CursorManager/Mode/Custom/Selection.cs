using System;
using UnityEngine;

namespace Code
{
    public class Selection : BaseMode<ModeSettings>
    {
        public event Action<Bounds> OnSelectionBox;

        private bool _isMousePressed;
        
        private Vector3 _mouseBegin;

        public override void OnDrop()
        {
            CursorManager.SelectionBox.Show(false);
        }

        private void Begin()
        {
            _isMousePressed = true;
            _mouseBegin = CursorManager.MousePosition;
            CursorManager.SelectionBox.Show(true);
        }

        private void End()
        {
            _isMousePressed = false;
            _mouseBegin = Vector3.zero;
            CursorManager.SelectionBox.Show(false);
        }

        private void CalculateBox()
        {
            var r = CursorManager.SelectionBox.Rect;
            var a = _mouseBegin;
            var b = CursorManager.MousePosition;
            var rx = a.x > b.x;
            var ry = a.y > b.y;
            
            r.position = a;

            r.sizeDelta = new Vector2(
                rx ? 
                    a.x - b.x : 
                    b.x - a.x,
                ry ? 
                    a.y - b.y : 
                    b.y - a.y
            );
            
            r.localScale = new Vector3(
                rx ? -1 : 1,
                ry ? -1 : 1,
                0);
        }

        private Bounds CalculateBounds()
        {
            var cam = CursorManager.TargetCamera;
            var (ra, rb) = 
                (cam.ScreenPointToRay(_mouseBegin), cam.ScreenPointToRay(CursorManager.MousePosition));
            var (rd, rl) = (CursorManager.RaycastDistance, CursorManager.CursorMask);

            if (Physics.Raycast(ra, out var ha, rd, rl) &&
                Physics.Raycast(rb, out var hb, rd, rl))
            {
                var (a, b) = (ha.point, hb.point);
                var (min, max) = 
                    (Vector3.Min(a, b), Vector3.Max(a, b) + Vector3.up);
                
                var bounds = new Bounds();
                bounds.SetMinMax(min, max);
                return bounds;
            }

            /*
             * Тут в апдейте будут спамиться Bounds, не очень хорошо для GC,
             * но на этапе прототипирования это будет не критично
             * Как вариант, использовать один объект Bounds и перенастраивать
             * его через SetMinMax
             */ 
            return new Bounds();
        }

        public override void OnUpdate(float deltaTime)
        {
            if (CursorManager.IsMouseDown)
                Begin();

            /*
             * Никогда не задумывался, могут ли события MouseDown и MouseUp
             * вызваться в одном кадре, поэтому без else
             */
            if (CursorManager.IsMouseUp)
                End();

            if (_isMousePressed)
            {
                CalculateBox();
                OnSelectionBox?.Invoke(CalculateBounds());
            }
        }
    }
}