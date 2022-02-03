namespace Code
{
    public abstract class BaseCommand
    {
        public abstract void Execute();
        // Можно абстрагировать ещё как Undoable command, но я пока не стал
        public abstract void Undo();
    }
}