using QuickFun.Domain.Enums;

namespace QuickFun.Games.Engines
{
    public class SnakeEngine : IGameEngine
    {
        public GameType Type => GameType.Snake;
        public string Name => "Wąż";
        public int Score { get; private set; } = 0;

        public void Reset()
        {
            Score = 0;
            // Reset pozycji węża
        }

        // Metody specyficzne dla węża
        public void MoveSnake() { /* logika */ }
    }
}