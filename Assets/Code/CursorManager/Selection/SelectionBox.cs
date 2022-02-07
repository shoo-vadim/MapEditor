using UnityEngine;

namespace Code
{
    public class SelectionBox : MonoBehaviour
    {
        public RectTransform Rect => rect;

        [SerializeField] 
        private RectTransform rect;
    }
}