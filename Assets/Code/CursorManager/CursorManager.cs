using System;
using UnityEngine;

namespace Code
{
    public class CursorManager : MonoBehaviour
    {
        public event Action<Shape, Vector3> OnShapeAdded;
        
        public const float RaycastDistance = 1000f;
        
        public const int ControlMouseButton = 0;

        public SelectionBox SelectionBox => selectionBox;

        public BaseCursor[] Cursors => cursors;

        public Camera TargetCamera => targetCamera;

        public LayerMask CursorMask => cursorMask;
        
        public bool IsMouseDown => Input.GetMouseButtonDown(ControlMouseButton);
        
        public bool IsMouseUp => Input.GetMouseButtonUp(ControlMouseButton);
        
        [SerializeField]
        private Camera targetCamera;
        
        [SerializeField] 
        private LayerMask cursorMask;
        
        [SerializeField] 
        private SelectionBox selectionBox;

        [SerializeField] 
        private BaseCursor[] cursors;
        
        // private CursorModeKind _currentCursorMode;
        // private GameObject _cursorPrefab;

        // private bool _isCursorShown;
        
        // private bool _isMouseDown;

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
        
        // Пускай тут полежат, вместо App
        // private CursorModeKind MapCursorFromShape(Shape shape) => shape switch 
        // {
        //     Shape.Sphere => CursorModeKind.Sphere, 
        //     Shape.Cube => CursorModeKind.Cube, 
        //     Shape.Cylinder => CursorModeKind.Cylinder, 
        //     _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, 
        //         $"Unable to select appropriate cursor for {shape} shape")
        // };
        //
        // private Shape MapShapeFromCursor(CursorModeKind mode) => mode switch
        // {
        //     CursorModeKind.Sphere => Shape.Sphere,
        //     CursorModeKind.Cube => Shape.Cube,
        //     CursorModeKind.Cylinder => Shape.Cylinder,
        //     _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, 
        //         $"Unable to select appropriate shape for {mode} cursor")
        // };

        // private void HandleMouseDown(Vector3 position)
        // {
        //     switch (CursorMode)
        //     {
        //         case CursorModeKind.Sphere:
        //         case CursorModeKind.Cube:
        //         case CursorModeKind.Cylinder:
        //             OnShapeAdded?.Invoke(MapShapeFromCursor(CursorMode), position);
        //             break;
        //         case CursorModeKind.Selection:
        //             Debug.Log("Selection click");
        //             break;
        //         case CursorModeKind.Off:
        //         default:
        //             throw new InvalidOperationException($"Unable to handle click in {CursorMode} mode");
        //     }
        // }

        // public void AddShape(Shape shape)
        // {
        //     CursorMode = MapCursorFromShape(shape);
        // }
        
        // Всегда прописываю Unity-messages как protected virtual, чтобы ненароком не перезаписать их при наследовании
        protected virtual void Update()
        {
            _currentMode?.OnUpdate(Time.deltaTime);
            // if (_currentCursorMode != CursorModeKind.Off) DrawCursor();
        }

        /*
         * Заметил, что в разных режимах этот функционал переиспользуется,
         * Плюс, так можно вывести работу со статическим Input,
         * чтобы в дальнейшем было проще написать заглушку для тестирования  
         */
        

        // private void HidePrefabs()
        // {
        //     foreach (var modePrefab in prefabs) 
        //         modePrefab.Prefab.SetActive(false);
        //
        //     _cursorPrefab = null;
        // }

        // private void DrawCursor()
        // {
        //     var ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        //     if (Physics.Raycast(ray, out var hit, RaycastDistance, cursorMask))
        //     {
        //         if (!_isCursorShown)
        //         {
        //             // Откешировал на всякий случай, но не уверен, что это нужно, возможно SetActive автоматом кеширует
        //             _isCursorShown = true;
        //             _cursorPrefab.SetActive(true);
        //         }
        //         
        //         // Тут как-то нужно разруливать MouseDown отдельно для стейта 
        //         // Получается? У нас тут три стейта Off, Selection, Adding
        //         if (Input.GetMouseButtonDown(ControlMouseButton) && !_isMouseDown)
        //         {
        //             _isMouseDown = true;
        //             HandleMouseDown(hit.point);
        //         }
        //         else if (Input.GetMouseButtonUp(ControlMouseButton) && _isMouseDown)
        //         {
        //             _isMouseDown = false;
        //         }
        //         
        //         _cursorPrefab.transform.position = hit.point;
        //     }
        //     else if (_isCursorShown)
        //     {
        //         _isCursorShown = false;
        //         _cursorPrefab.SetActive(false);
        //     }
        // }
    }
}