using System.Collections.Generic;
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
        
        private readonly List<MonoShape> Scene = new();

        // По-идее, между Cursor и Control должна быть своя прослойка, но пусть пока этим занимается App
        private void OnShapeControl(Shape shape) =>
            cursorManager.Use<Addition, AdditionSettings>(new AdditionSettings(shape));

        private void OnSelectionControl() =>
            cursorManager.Use<Selection, ModeSettings>(new ModeSettings());

        private void OnShapeAdded(Shape shape, Vector3 position)
        {
            var monoShape = shapePool.Obtain(shape);
            Scene.Add(monoShape);
            monoShape.transform.position = position;
        }
        
        // Вызывается в OnUpdate в режиме выделения
        private void OnSelectionBox(Bounds bounds)
        {
            var selected = new List<MonoShape>();
            foreach (var shape in Scene)
                if (bounds.Intersects(shape.Bounds))
                    selected.Add(shape);
            Debug.Log(selected.Count);
        }

        /*
         * Тут возможно слишком увлекся валидацией нужных объектов для тестового задания,
         * но считаю что она крайне важна, иначе будут попадаться null-ref в рантайме, а не на компиляции.
         * Способы для валидации могут быть разные, я сделал самым примитивным.
         * Ещё можно написать кастомный аттрибут, либо делегировать отслеживание зависимостей DI-контейнеру
         */
        private void OnValidate()
        {
            if (control == null) 
                control = this.Detect<Control>();
            if (cursorManager == null)
                cursorManager = this.Detect<CursorManager>();
            if (shapePool == null)
                shapePool = this.Detect<ShapePool>();
        }

        protected virtual void Start() =>
            cursorManager.Use<Off, ModeSettings>(new ModeSettings());

        protected virtual void OnEnable() => Subscribe();

        protected virtual void OnDisable() => Unsubscribe();

        private void Subscribe()
        {
            control.OnSelection += OnSelectionControl;
            control.OnShape += OnShapeControl;
            cursorManager.OnShapeAdded += OnShapeAdded;
            cursorManager.OnSelectionBox += OnSelectionBox;
        }

        private void Unsubscribe()
        {
            control.OnSelection -= OnSelectionControl;
            control.OnShape -= OnShapeControl;
            cursorManager.OnShapeAdded -= OnShapeAdded;
            cursorManager.OnSelectionBox -= OnSelectionBox;
        }
    }
}
