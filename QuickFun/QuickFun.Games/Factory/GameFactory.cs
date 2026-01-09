using QuickFun.Domain.Enums;
using QuickFun.Games.Engines;
using QuickFun.Games.Memory;
using Microsoft.Extensions.Http;
using System.Net.Http;
using QuickFun.Games.Engines.Sudoku;
using QuickFun.Games.Minesweeper;

namespace QuickFun.Games
{
    public interface IGameFactory
    {
        IGameEngine CreateGame(GameType type);
    }

    public class GameFactory : IGameFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GameFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IGameEngine CreateGame(GameType type)
        {
            return type switch
            {
                GameType.TicTacToe => new TicTacToeEngine(),
                GameType.Memory => new MemoryEngine(),
                GameType.Minesweeper => new MinesweeperEngine(new QuickFun.Games.Minesweeper.Strategies.DfsFloodingStrategy()),
                GameType.Snake => new SnakeEngine(),
                GameType.Sudoku => new SudokuEngine(_httpClientFactory.CreateClient("SudokuApi")),
                _ => throw new ArgumentException("Nieznana gra")
            };
        }
    }
}