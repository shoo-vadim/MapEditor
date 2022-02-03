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

        // public event Action<Vector3> OnClick;
        public event Action<Shape, Vector3> OnShapeAdded; 

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
        private Camera targetCamera;
        
        [SerializeField] 
        private LayerMask cursorMask;

        private CursorMode _currentCursorMode;
        private GameObject _cursorPrefab;

        private bool _isCursorShown;
        
        private bool _isMouseDown;

        private const float RaycastDistance = 1000f;
        private const int ControlMouseButton = 0;
        
        // Пускай тут полежат, вместо App
        private CursorMode MapCursorFromShape(Shape shape) => shape switch 
        {
            Shape.Sphere => CursorMode.Sphere, 
            Shape.Cube => CursorMode.Cube, 
            Shape.Cylinder => CursorMode.Cylinder, 
            _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, 
                $"Unable to select appropriate cursor for {shape} shape")
        };

        private Shape MapShapeFromCursor(CursorMode mode) => mode switch
        {
            CursorMode.Sphere => Shape.Sphere,
            CursorMode.Cube => Shape.Cube,
            CursorMode.Cylinder => Shape.Cylinder,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, 
                $"Unable to select appropriate shape for {mode} cursor")
        };

        private void HandleClick(Vector3 position)
        {
            switch (CursorMode)
            {
                case CursorMode.Sphere:
                case CursorMode.Cube:
                case CursorMode.Cylinder:
                    OnShapeAdded?.Invoke(MapShapeFromCursor(CursorMode), position);
                    break;
                case CursorMode.Selection:
                    Debug.Log("Selection click");
                    break;
                case CursorMode.Off:
                default:
                    throw new InvalidOperationException($"Unable to handle click in {CursorMode} mode");
            }
        }

        public void AddShape(Shape shape)
        {
            CursorMode = MapCursorFromShape(shape);
        }
        
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
            var ray = targetCamera.ScreenPointToRay(Input.mousePosition);
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
                    HandleClick(hit.point);
                    // OnClick?.Invoke(hit.point);
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