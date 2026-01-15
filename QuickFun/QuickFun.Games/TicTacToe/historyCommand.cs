using System.Collections.Generic;

namespace QuickFun.Games.Engines.TicTacToe.AI
{
    public class CommandHistory
    {
        private readonly Stack<ICommand> _history = new Stack<ICommand>();

        public void ExecuteCommand(ICommand cmd)
        {
            cmd.Execute();
            _history.Push(cmd);
        }

        public void UndoLast()
        {
            if (_history.Count > 0)
            {
                var cmd = _history.Pop();
                cmd.Undo();
            }
        }

        public void Clear() => _history.Clear();
        public bool CanUndo => _history.Count > 0;
    }
}