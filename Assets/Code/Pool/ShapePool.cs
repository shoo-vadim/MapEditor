using System;

namespace Code.Pool
{
    /*
     * Возможно я погорячился, и тут не нужно указывать TRequest.
     * Я бы посмотрел как лучше, как только закончу основной функционал.
     * На самом деле, сейчас даже Release в пул не проверен
     */
    public class ShapePool : PrefabedPool<Shape, MonoShape>
    {
        // TODO: Нужно будет перепроверить выдасться ли базовый тип при таком сравнении.
        // => Вроде работает, но отдельно на условном sharplab не проверял 
        public override MonoShape Obtain(Shape request)
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