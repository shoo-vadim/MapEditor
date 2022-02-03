using Code.Pool;

namespace Code.Commands
{
    public abstract class AppCommand : BaseCommand
    {
        protected ShapePool shapePool;

        protected AppCommand(ShapePool shapePool)
        {
            this.shapePool = shapePool;
        }
    }
}