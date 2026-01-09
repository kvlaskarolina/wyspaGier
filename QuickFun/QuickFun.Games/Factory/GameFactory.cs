using QuickFun.Domain.Enums;
using QuickFun.Games.Engines;
using QuickFun.Games.Memory;
using QuickFun.Games.Minesweeper;

namespace QuickFun.Games
{
    public interface IGameFactory
    {
        IGameEngine CreateGame(GameType type);
    }

    public class GameFactory : IGameFactory
    {
        public IGameEngine CreateGame(GameType type)
        {
            return type switch
            {
                GameType.TicTacToe => new TicTacToeEngine(),
                GameType.Memory => new MemoryEngine(),
                GameType.Minesweeper => new MinesweeperEngine(new QuickFun.Games.Minesweeper.Strategies.DfsFloodingStrategy()),
                GameType.Snake => new SnakeEngine(),
                _ => throw new ArgumentException("Nieznana gra")
            };
        }
    }
}