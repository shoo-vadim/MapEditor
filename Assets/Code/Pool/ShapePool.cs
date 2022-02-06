using System;

namespace Code.Pool
{
    public class ShapePool : MonoPool<MonoShape>
    {
        public MonoShape Obtain(Shape shape)
        {
            /*
             * Тут можно выдавать фигуру через pattern matching и делать упор
             * на проверку компилятором, дописав Roslyn скрипты например.
             * Либо, забиндить через, например поле Shape, на каждом префабе, а потом валидировать через редактор
             */
            return shape switch
            {
                Shape.Sphere => Obtain<SphereShape>(),
                Shape.Cube => Obtain<CubeShape>(),
                Shape.Cylinder => Obtain<CylinderShape>(),
                _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, 
                    $"Unable to find type for shape {shape}")
            };
        }
    }
}