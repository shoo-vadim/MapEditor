using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    // Не стал создавать промежуточный класс/интерфейс для executor
    public class MonoExecutor : MonoBehaviour
    {
        private Stack<BaseCommand> StackRedo => new Stack<BaseCommand>();
        private Stack<BaseCommand> StackUndo => new Stack<BaseCommand>();

        protected void Execute(BaseCommand command, bool clearRedoStack = true)
        {
            if (clearRedoStack)
                StackRedo.Clear();
            command.Execute();
            StackUndo.Push(command);
        }

        protected bool Undo()
        {
            if (StackUndo.Count == 0) return false;
            Execute(StackUndo.Pop(), false);
            return true;
        }
    }
}