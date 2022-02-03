using Code.Pool;
using UnityEngine;

namespace Code
{
    public class App : MonoExecutor
    {
        [SerializeField] 
        private CursorManager cursorManager;
        
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
        // Не очень удобно передавать каждый раз cursorManager, но что-то мне не приходит в голову,
        // как это можно упростить, т.к. в некоторые Mode мы должны передавать доп параметры
        private void OnShapeControl(Shape shape) => 
            cursorManager.Use(new Addition(cursorManager, shape));
        
        private void OnSelectionControl() =>
            cursorManager.Use(new Selection(cursorManager));
        
        /*
         * Вообще, увлекся и начал сахарить поиск нужных объектов.
         * Можно было бы даже написать отдельный аттрибут, чтобы им помечать необходимые поля
         */
        private void OnValidate()
        {
            if (control == null) 
                control = Components.Find<Control>(gameObject);
            if (cursorManager == null) 
                cursorManager = Components.Find<CursorManager>(gameObject);
            if (shapePool == null)
                shapePool = Components.Find<ShapePool>(gameObject);
        }
        
        protected virtual void Start() => 
            cursorManager.Use(new Off(cursorManager));

        protected virtual void OnEnable() => Subscribe();

        protected virtual void OnDisable() => Unsubscribe();

        private void Subscribe()
        {
            control.OnSelection += OnSelectionControl;
            control.OnShape += OnShapeControl;
            cursorManager.OnShapeAdded += OnShapeAdded;
        }

        private void Unsubscribe()
        {
            control.OnSelection -= OnSelectionControl;
            control.OnShape -= OnShapeControl;
            cursorManager.OnShapeAdded -= OnShapeAdded;
        }
    }
}
