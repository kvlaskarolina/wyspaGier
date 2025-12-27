using QuickFun.Domain.Enums;

namespace QuickFun.Games.Engines
{
    public class TicTacToeEngine : IGameEngine
    {
        // Implementacja interfejsu IGameEngine
        public GameType Type => GameType.TicTacToe;
        public string Name => "Kółko i Krzyżyk";
        public int Score { get; private set; } = 0;

        // Logika specyficzna dla Kółka i Krzyżyk
        public char[] Board { get; private set; } = new char[9]; // 9 pól: 0-8
        public char CurrentPlayer { get; private set; } = 'X';
        public bool IsGameOver { get; private set; }
        public string Message { get; private set; } = "Zaczyna X";

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
            // Jeśli pole zajęte lub koniec gry - nic nie rób
            if (IsGameOver || Board[index] != '\0') return;

            Board[index] = CurrentPlayer;

            if (CheckWinner())
            {
                Message = $"Wygrał gracz: {CurrentPlayer}!";
                IsGameOver = true;
                Score = 1; // Możesz przyznać 1 punkt za wygraną
            }
            else if (Board.All(c => c != '\0'))
            {
                Message = "Remis!";
                IsGameOver = true;
                Score = 0;
            }
            else
            {
                // Zmiana gracza
                CurrentPlayer = (CurrentPlayer == 'X') ? 'O' : 'X';
                Message = $"Ruch gracza: {CurrentPlayer}";
            }
        }

        private bool CheckWinner()
        {
            // Możliwe kombinacje wygrywające
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