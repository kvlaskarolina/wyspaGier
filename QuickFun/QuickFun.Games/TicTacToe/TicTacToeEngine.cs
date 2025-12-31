using QuickFun.Domain.Enums;

namespace QuickFun.Games.Engines
{
    public class TicTacToeEngine : IGameEngine
    {
        public GameType Type => GameType.TicTacToe;
        public string Name => "Kółko i Krzyżyk";
        public int Score { get; protected set; } = 0;
        public char[] Board { get; protected set; } = new char[9];
        public char CurrentPlayer { get; protected set; } = 'X';
        public bool IsGameOver { get; protected set; }
        public string Message { get; protected set; } = "Zaczyna X";

        public TicTacToeEngine()
        {
            Reset();
        }

        public void Reset()
        {
            Board = new char[9]; // puste znaki
            CurrentPlayer = 'X';
            IsGameOver = false;
            Score = 0;
            Message = "Zaczyna X";
        }

        public void MakeMove(int index)
        {
            if (IsGameOver || Board[index] != '\0') return;

            Board[index] = CurrentPlayer;

            if (CheckWinner())
            {
                Message = $"Wygrał gracz: {CurrentPlayer}!";
                IsGameOver = true;
                Score = 1;
            }
            else if (Board.All(c => c != '\0'))
            {
                Message = "Remis!";
                IsGameOver = true;
                Score = 0;
            }
            else
            {
                CurrentPlayer = (CurrentPlayer == 'X') ? 'O' : 'X';
                Message = $"Ruch gracza: {CurrentPlayer}";
            }
        }

        protected bool CheckWinner()
        {
            int[][] winners = new int[][]
            {
                new[] {0,1,2}, new[] {3,4,5}, new[] {6,7,8}, // Poziomo
                new[] {0,3,6}, new[] {1,4,7}, new[] {2,5,8}, // Pionowo
                new[] {0,4,8}, new[] {2,4,6}             // Skosy
            };

            foreach (var w in winners)
            {
                if (Board[w[0]] != '\0' &&
                    Board[w[0]] == Board[w[1]] &&
                    Board[w[0]] == Board[w[2]])
                {
                    return true;
                }
            }
            return false;
        }
    }
}