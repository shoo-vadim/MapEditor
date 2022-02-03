using System;
using UnityEngine;

namespace Code
{
    public class Control : MonoBehaviour
    {
        public event Action OnSelection;
        
        public event Action<Shape> OnShape;

        [SerializeField] 
        private BaseButton buttonUndo;
        
        [SerializeField] 
        private BaseButton buttonRedo;

        [SerializeField] 
        private BaseButton buttonSelection;
        
        [SerializeField] 
        private BaseButton buttonSphere;
        
        [SerializeField] 
        private BaseButton buttonCube;
        
        [SerializeField] 
        private BaseButton buttonCylinder;
        
        private void OnUndoClick()
        {
            Debug.Log(nameof(OnUndoClick));
        }
        
        private void OnRedoClick()
        {
            Debug.Log(nameof(OnRedoClick));
        }

        private void OnSelectionClick() => OnSelection?.Invoke();

        private void OnSphereClick() => OnShape?.Invoke(Shape.Sphere);

        private void OnCubeClick() => OnShape?.Invoke(Shape.Cube);
        
        private void OnCylinderClick() => OnShape?.Invoke(Shape.Cylinder);

        private void Subscribe()
        {
            buttonUndo.OnClick += OnUndoClick;
            buttonRedo.OnClick += OnRedoClick;
            buttonSelection.OnClick += OnSelectionClick;
            buttonSphere.OnClick += OnSphereClick;
            buttonCube.OnClick += OnCubeClick;
            buttonCylinder.OnClick += OnCylinderClick;            
        }

        private void UnSubscribe()
        {
            buttonUndo.OnClick -= OnUndoClick;
            buttonRedo.OnClick -= OnRedoClick;
            buttonSelection.OnClick -= OnSelectionClick;
            buttonSphere.OnClick -= OnSphereClick;
            buttonCube.OnClick -= OnCubeClick;
            buttonCylinder.OnClick -= OnCylinderClick;
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void OnValidate()
        {
            if (buttonUndo == null) 
                buttonUndo = Components.Find<UndoButton>(gameObject);
            if (buttonRedo == null) 
                buttonRedo = Components.Find<UndoButton>(gameObject);
            if (buttonSelection == null) 
                buttonSelection = Components.Find<UndoButton>(gameObject);
            if (buttonSphere == null) 
                buttonSphere = Components.Find<UndoButton>(gameObject);
            if (buttonCube == null) 
                buttonCube = Components.Find<UndoButton>(gameObject);
            if (buttonCylinder == null) 
                buttonCylinder = Components.Find<UndoButton>(gameObject);
        }
    }
}
