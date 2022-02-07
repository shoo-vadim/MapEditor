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

        /*
         * Просто для примера, GO сам собирает нужные себе зависимости с префаба,
         * и краснит в консоль, если они не найдены.
         */
        private void OnValidate()
        {
            if (buttonUndo == null) 
                buttonUndo = this.Detect<UndoButton>();
            if (buttonRedo == null) 
                buttonRedo = this.Detect<RedoButton>();
            if (buttonSelection == null) 
                buttonSelection = this.Detect<SelectionButton>();
            if (buttonSphere == null) 
                buttonSphere = this.Detect<SphereButton>();
            if (buttonCube == null) 
                buttonCube = this.Detect<CubeButton>();
            if (buttonCylinder == null) 
                buttonCylinder = this.Detect<CylinderButton>();

        }
    }
}
