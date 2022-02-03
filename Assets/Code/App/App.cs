using System;
using Code.Pool;
using UnityEngine;

namespace Code
{
    public class App : MonoExecutor
    {
        [SerializeField] 
        private Cursor cursor;
        
        [SerializeField] 
        private Control control;

        [SerializeField] 
        private ShapePool shapePool;
        
        /*
         * Как будет обрабатываться селекшн?
         * Можно при добавлении объектов из пула, подписываться на их OnSelected,
         * Можно сделать мультиселект, и тогда скорее всего селекшн уйдет в монобех Cursor
         */
        private void AddShape(Shape shape, Vector3 position)
        {
            var monoShape = shapePool.Obtain(shape);
            monoShape.transform.position = position;
        }

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

        private void OnShapeControl(Shape shape)
        {
            /*
             * Я не особенно запаривался с мэппингом shape->cursor, так как он легко делается через паттерн-матчинг
             * Конечно, тут нужно продумывать какой-то механизм, либо краснить roslyn'ом,
             * если мы добавили новый элемент и мэппинг для него не прописан
             */
            cursor.CursorMode = MapCursorFromShape(shape);
        }
        
        private void OnSelectionControl()
        {
            cursor.CursorMode = CursorMode.Selection;
        }

        private void OnCursorClick(Vector3 position)
        {
            /*
             * Возможно, не самое удачное решение привязываться к состоянию курсора,
             * но для ускорения и чистоты написания кода сделал так.
             * Выше видно как через PM все мэппится туда-обратно
             */
            switch (cursor.CursorMode)
            {
                case CursorMode.Sphere:
                case CursorMode.Cube:
                case CursorMode.Cylinder:
                    AddShape(MapShapeFromCursor(cursor.CursorMode), position);
                    break;
                case CursorMode.Selection:
                    Debug.Log("Selection click");
                    break;
                case CursorMode.Off:
                default:
                    throw new InvalidOperationException($"Unable to handle click in {cursor.CursorMode} mode");
            }
        }

        /*
         * Вообще, увлекся и начал сахарить поиск нужных объектов.
         * Можно было бы даже написать отдельный аттрибут, чтобы им помечать необходимые поля
         */
        private void OnValidate()
        {
            if (control == null) 
                control = Components.Find<Control>(gameObject);
            if (cursor == null) 
                cursor = Components.Find<Cursor>(gameObject);
            if (shapePool == null)
                shapePool = Components.Find<ShapePool>(gameObject);
        }
        
        private void Start()
        {
            cursor.CursorMode = CursorMode.Off;
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            control.OnSelection += OnSelectionControl;
            control.OnShape += OnShapeControl;
            cursor.OnClick += OnCursorClick;
        }

        private void Unsubscribe()
        {
            control.OnSelection -= OnSelectionControl;
            control.OnShape -= OnShapeControl;
            cursor.OnClick -= OnCursorClick;
        }

    }
}
