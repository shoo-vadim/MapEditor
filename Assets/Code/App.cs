using UnityEngine;

namespace Code
{
    public class App : MonoBehaviour
    {
        [SerializeField] 
        private Camera mainCamera;
        
        [SerializeField] 
        private GameObject cursor;
        
        [SerializeField] 
        private LayerMask cursorMask;

        private bool _isCursorShown;

        private const float RaycastDistance = 1000f;
        
        private void Update()
        {
            DrawCursor();
        }

        private void DrawCursor()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, RaycastDistance, cursorMask))
            {
                if (!_isCursorShown)
                {
                    // Откешировал на всякий случай, но не уверен, что это нужно, возможно SetActive автоматом кеширует
                    _isCursorShown = true;
                    cursor.SetActive(true);
                }
                
                cursor.transform.position = hit.point;
            }
            else if (_isCursorShown)
            {
                _isCursorShown = false;
                cursor.SetActive(false);
            }
        }
    }
}
