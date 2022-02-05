using UnityEngine;

namespace Code
{
    public abstract class BaseCursor : MonoBehaviour
    {
        public void Show(bool visibility)
        {
            gameObject.SetActive(visibility);
        }
    }
}