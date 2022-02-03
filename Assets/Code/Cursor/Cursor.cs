using System;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class Cursor : MonoBehaviour
    {
        [Serializable]
        private class ModePrefab
        {
            [SerializeField]
            private CursorMode cursorMode;
            
            [SerializeField]
            private GameObject prefab;

            public CursorMode CursorMode => cursorMode;

            public GameObject Prefab => prefab;
        }

        public event Action<Vector3> OnClick;

        public CursorMode CursorMode
        {
            get => _currentCursorMode;
            set
            {
                HidePrefabs();
                
                if (value != CursorMode.Off)
                {
                    _cursorPrefab = prefabs.FirstOrDefault(m => m.CursorMode == value)?.Prefab;
                    if (_cursorPrefab == null) 
                        throw new NullReferenceException($"Unable to find prefab for mode {value}");
                    
                    _cursorPrefab.SetActive(true);
                }

                _currentCursorMode = value;
            }
        }

        [SerializeField] 
        private ModePrefab[] prefabs;

        [SerializeField] 
        private Camera mainCamera;
        
        [SerializeField] 
        private LayerMask cursorMask;

        private CursorMode _currentCursorMode;
        private GameObject _cursorPrefab;

        private bool _isCursorShown;
        
        [SerializeField]
        private bool _isMouseDown;

        private const float RaycastDistance = 1000f;
        private const int ControlMouseButton = 0;

        private void Update()
        {
            if (_currentCursorMode != CursorMode.Off) DrawCursor();
        }

        private void HidePrefabs()
        {
            foreach (var modePrefab in prefabs) 
                modePrefab.Prefab.SetActive(false);

            _cursorPrefab = null;
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
                    _cursorPrefab.SetActive(true);
                }
                
                if (Input.GetMouseButtonDown(ControlMouseButton) && !_isMouseDown)
                {
                    _isMouseDown = true;
                    OnClick?.Invoke(hit.point);
                }
                else if (Input.GetMouseButtonUp(ControlMouseButton) && _isMouseDown)
                {
                    _isMouseDown = false;
                    
                }
                
                _cursorPrefab.transform.position = hit.point;
            }
            else if (_isCursorShown)
            {
                _isCursorShown = false;
                _cursorPrefab.SetActive(false);
            }
        }
    }
}