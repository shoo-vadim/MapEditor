using System;
using UnityEngine;

namespace Code.State
{
    [Serializable]
    public record ShapeProps(Vector3 Scale, Color Color) : IProps;
}