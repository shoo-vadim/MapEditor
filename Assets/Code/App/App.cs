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
        
        private void OnShapeAdded(Shape shape, Vector3 position)
        {
            var monoShape = shapePool.Obtain(shape);
            monoShape.transform.position = position;
        }
        
        // По-идее, между Cursor и Control должна быть своя прослойка, но пусть пока этим занимается App
        private void OnShapeControl(Shape shape)
        {
            cursor.AddShape(shape);
        }
        
        private void OnSelectionControl()
        {
            cursor.CursorMode = CursorMode.Selection;
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
            cursor.OnShapeAdded += OnShapeAdded;
        }

        private void Unsubscribe()
        {
            control.OnSelection -= OnSelectionControl;
            control.OnShape -= OnShapeControl;
            cursor.OnShapeAdded -= OnShapeAdded;
        }

    }
}
