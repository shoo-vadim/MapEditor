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
        // Не очень удобно передавать каждый раз cursorManager, но что-то мне не приходит в голову,
        // как это можно упростить, т.к. в некоторые Mode мы должны передавать доп параметры
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

        private void OnSelectionBox(Bounds bounds)
        {
            var selected = new List<MonoShape>();
            foreach (var shape in Scene)
                if (bounds.Intersects(shape.Renderer.bounds))
                    selected.Add(shape);
            Debug.Log(selected.Count);
        }

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
