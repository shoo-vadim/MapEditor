using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    /*
     * Этот компонент нужен чтобы более-менее затипизировать UI,
     * при OnValidate основная панель будет собирать кнопки, и если что-то не так, то будет краснить.
     * Благодаря этому мы минимизируем ошибки сборки сцены, не вынося это в рантайм.
     */
    [RequireComponent(typeof(Button))]
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] 
        private Button target;
        
        public event Action OnClick;

        private void OnEnable()
        {
            // Не стал выводить в отдельный метод, т.к. есть RemoveAllListeners
            target.onClick.AddListener(() => OnClick?.Invoke());
        }

        private void OnDisable()
        {
            target.onClick.RemoveAllListeners();
        }

        private void OnValidate()
        {
            if (target == null) target = GetComponent<Button>();
            if (target == null) throw new ArgumentException("Unable to find button component");
        }
    }
}