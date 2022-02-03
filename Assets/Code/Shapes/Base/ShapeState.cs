using System;
using UnityEngine;

namespace Code.State
{
    [Serializable]
    public record ShapeState(Vector3 Scale, Color Color) : IState;
}