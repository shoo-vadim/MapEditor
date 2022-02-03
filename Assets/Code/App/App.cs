using System;
using UnityEngine;

namespace Code
{
    public class App : MonoBehaviour// , IPointerClickHandler
    {
        [SerializeField] 
        private Cursor cursor;
        
        [SerializeField] 
        private Control control;

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
        }

        private void Unsubscribe()
        {
            control.OnSelection += OnSelectionControl;
            control.OnShape += OnShapeControl;
        }

        private void OnShapeControl(Shape shape)
        {
            // Я не особенно запаривался с мэппингом shape->cursor, так как он легко делается через паттерн-матчинг
            cursor.CursorMode = shape switch
            {
                Shape.Sphere => CursorMode.Sphere,
                Shape.Cube => CursorMode.Cube,
                Shape.Cylinder => CursorMode.Cylinder,
                _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, 
                    $"Unable to select appropriate cursor for {shape} shape")
            };
        }
        
        private void OnSelectionControl()
        {
            cursor.CursorMode = CursorMode.Selection;
        }

        private void OnValidate()
        {
            if (control == null) 
                control = Services.Find<Control>(gameObject);
            if (cursor == null) 
                cursor = Services.Find<Cursor>(gameObject);
        }
    }
}
