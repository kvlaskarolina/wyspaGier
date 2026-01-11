using QuickFun.Games.TicTacToe.Strategies;
using QuickFun.Domain.Enums;
using QuickFun.Games.Engines;
using System.Reflection;

namespace QuickFun.Games.Engines.TicTacToe.AI
{

    public class TicTacToeEngineWithAI : TicTacToeEngine
    {
        private ITicTacToeDifficultyStrategy _aiStrategy;

        public TicTacToeEngineWithAI(ITicTacToeDifficultyStrategy strategy)
        {
            _aiStrategy = strategy;
        }

        public TicTacToeEngineWithAI()
        {
            _aiStrategy = new TicTacToeMediumStrategy();
        }

        public new void Reset()
        {
            base.Reset();
            if (_aiStrategy == null)
            {
                _aiStrategy = new TicTacToeMediumStrategy();
            }
        }

        public void ResetGame()
        {
            Reset();
        }

        public void SetDifficulty(Level level)
        {
            _aiStrategy = level switch
            {
                Level.Easy => new TicTacToeEasyStrategy(),
                Level.Medium => new TicTacToeMediumStrategy(),
                Level.Hard => new TicTacToeHardStrategy(),
                _ => new TicTacToeMediumStrategy()
            };
        }

        public new async Task MakeMove(int index)
        {
            if (IsGameOver || index < 0 || index >= Board.Length || Board[index] != '\0')
                return;

            Board[index] = CurrentPlayer;

            if (CheckWinner())
            {
                Message = "Wygrałeś!";
                IsGameOver = true;
                Score = 1;
                return;
            }

            if (IsBoardFull())
            {
                Message = "Remis!";
                IsGameOver = true;
                Score = 0;
                return;
            }

            CurrentPlayer = 'O';

            await Task.Delay(500);
            Message = "";
            int aiMove = MakeAIMove();

            if (aiMove != -1)
            {
                Board[aiMove] = CurrentPlayer;

                if (CheckWinner())
                {
                    Message = "Przegrałeś!";
                    IsGameOver = true;
                    Score = -1;
                    return;
                }

                if (IsBoardFull())
                {
                    Message = "Remis!";
                    IsGameOver = true;
                    Score = 0;
                    return;
                }

                CurrentPlayer = 'X';


            }
        }

        private int MakeAIMove()
        {
            int[][] boardForAI = ConvertBoardForAI();
            return _aiStrategy.MakeAIMove(boardForAI);
        }

        private int[][] ConvertBoardForAI()
        {
            int[][] board2D = new int[3][];
            for (int i = 0; i < 3; i++)
            {
                board2D[i] = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    int index = i * 3 + j;
                    board2D[i][j] = Board[index] switch
                    {
                        '\0' => 0,  // Puste pole
                        'X' => 1,   // Gracz
                        'O' => 2,   // AI
                        _ => 0
                    };
                }
            }
            return board2D;
        }

        private bool IsBoardFull()
        {
            return Board.All(c => c != '\0');
        }
    }
}