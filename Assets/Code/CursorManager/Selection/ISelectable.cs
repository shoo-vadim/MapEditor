using UnityEngine;

namespace Code
{
    public interface ISelectable
    {
        Transform Anchor { get; }
        
        Bounds Bounds { get; }
    }
}