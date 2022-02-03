using System;

namespace Code.Pool
{
    public class ShapePool : PrefabedPool<Shape, BaseShape>
    {
        // TODO: Нужно будет перепроверить выдасться ли базовый тип при таком сравнении
        public override BaseShape Obtain(Shape request)
        {
            return ObtainInstance(request switch
            {
                Shape.Sphere => typeof(SphereShape),
                Shape.Cube => typeof(CubeShape),
                Shape.Cylinder => typeof(CylinderShape),
                _ => throw new ArgumentOutOfRangeException(nameof(request), request, $"Unable to find shape for request {request}")
            });
        }
    }
}