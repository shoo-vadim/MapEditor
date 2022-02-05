using UnityEngine;

namespace Code
{
    public class ShapeCursor : BaseCursor
    {
        public Shape Shape => shape;
        
        [SerializeField] 
        private Shape shape;
    }
}