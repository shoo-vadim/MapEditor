using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class Control : MonoBehaviour
    {
        // TODO: Добавить OnValidate
        [SerializeField] 
        private BaseButton buttonUndo;
        
        [SerializeField] 
        private BaseButton buttonRedo;
        
        [SerializeField] 
        private BaseButton buttonSphere;
        
        [SerializeField] 
        private BaseButton buttonCube;
        
        [SerializeField] 
        private BaseButton buttonCylinder;

        private TButton FindButton<TButton>() 
            where TButton : BaseButton
        {
            var button = GetComponentInChildren<TButton>();
            if (button == null) throw new ArgumentException($"Unable to find button of type {nameof(TButton)}");
            return button;
        }

        private void OnUndoClick()
        {
            Debug.Log(nameof(OnUndoClick));
        }
        
        private void OnRedoClick()
        {
            Debug.Log(nameof(OnRedoClick));
        }
        
        private void OnSphereClick()
        {
            Debug.Log(nameof(OnSphereClick));
        }
        
        private void OnCubeClick()
        {
            Debug.Log(nameof(OnCubeClick));
        }
        
        private void OnCylinderClick()
        {
            Debug.Log(nameof(OnCylinderClick));
        }

        private void Subscribe()
        {
            buttonUndo.OnClick += OnUndoClick;
            buttonRedo.OnClick += OnRedoClick;
            buttonSphere.OnClick += OnSphereClick;
            buttonCube.OnClick += OnCubeClick;
            buttonCylinder.OnClick += OnCylinderClick;            
        }

        private void UnSubscribe()
        {
            buttonUndo.OnClick -= OnUndoClick;
            buttonRedo.OnClick -= OnRedoClick;
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
            buttonUndo = FindButton<UndoButton>();
            buttonRedo = FindButton<RedoButton>();
            buttonSphere = FindButton<SphereButton>();
            buttonCube = FindButton<CubeButton>();
            buttonCylinder = FindButton<CylinderButton>();
        }
    }
}
