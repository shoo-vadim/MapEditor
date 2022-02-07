using UnityEngine;

namespace Code
{
    public abstract class BaseCursor : MonoBehaviour
    {
        /*
         * Здесь на самом деле очень условная логика.
         * Предположим, мы не знаем, кешируется ли SetActive по-умолчанию.
         * Для этого, мы кешируем данные в переменную _isShown
         * Но чтобы синхронизировать её, нужно изначально понять был ли
         * курсор изначально включен включен в сцене.
         * Чтобы не возникало затем путанницы, мы красним, когда кто-то пытается
         * работать с непроинициализированным курсором.
         * Это вложенно в рамки стандартной Unity инициализаци Awake/Start, для
         * более серьезного приложения, конечно нужно продумать систему инициализации
         * 
         * Закоментированно и оставленно в качестве примера кеширования/работы со стандартным пайпом Awake/Start
         */
        // private bool _isReady = false;
        // private bool _isShown;
        //
        // public void Show(bool visibility)
        // {
        //     if (!_isReady)
        //         throw new InvalidOperationException("Cursor not ready, please wait for Awake before using it");
        //
        //     if (_isShown == visibility) return;
        //     
        //     gameObject.SetActive(visibility);
        //     _isShown = visibility;
        // }
        //
        // protected virtual void Awake()
        // {
        //     _isShown = gameObject.activeSelf;
        //     _isReady = true;
        // }
    }
}