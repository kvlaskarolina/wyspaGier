namespace QuickFun.Games.TicTacToe.Strategies;

public enum Level
{
    Easy,
    Medium,
    Hard
}

public interface ITicTacToeDifficultyStrategy
{
    Level Level { get; }
    int MakeAIMove(int[][] board);
}

public class TicTacToeEasyStrategy : ITicTacToeDifficultyStrategy
{
    private readonly Random _random = new();

    public Level Level => Level.Easy;

    public int MakeAIMove(int[][] board)
    {
        return MakeAIMoveRandom(board);
    }

    private int MakeAIMoveRandom(int[][] board)
    {
        var availableMoves = new List<int>();

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] == 0)
                {
                    availableMoves.Add(i * 3 + j);
                }
            }
        }

        if (availableMoves.Count > 0)
        {
            return availableMoves[_random.Next(availableMoves.Count)];
        }

        return -1;
    }
}

public class TicTacToeMediumStrategy : ITicTacToeDifficultyStrategy
{
    private readonly Random _random = new();

    public Level Level => Level.Medium;

    public int MakeAIMove(int[][] board)
    {
        return _random.Next(2) == 0
            ? MakeAIMoveRandom(board)
            : MakeAIMoveSmart(board);
    }

    private int MakeAIMoveRandom(int[][] board)
    {
        var availableMoves = new List<int>();

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] == 0)
                {
                    availableMoves.Add(i * 3 + j);
                }
            }
        }

        if (availableMoves.Count > 0)
        {
            return availableMoves[_random.Next(availableMoves.Count)];
        }

        return -1;
    }

    private int MakeAIMoveSmart(int[][] board)
    {
        var winningMove = FindWinningMove(board, 2);
        if (winningMove != -1) return winningMove;

        var blockingMove = FindWinningMove(board, 1);

        if (blockingMove != -1) return blockingMove;

        return MakeAIMoveRandom(board);
    }

    private int FindWinningMove(int[][] board, int player)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i][j] == 0)
                {
                    board[i][j] = player;

                    if (CheckWin(board, player))
                    {
                        board[i][j] = 0;
                        return i * 3 + j;
                    }

                    board[i][j] = 0;
                }
            }
        }

        return -1;
    }

    private bool CheckWin(int[][] board, int player)
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i][0] == player && board[i][1] == player && board[i][2] == player)
                return true;
        }

        for (int j = 0; j < 3; j++)
        {
            if (board[0][j] == player && board[1][j] == player && board[2][j] == player)
                return true;
        }

        if (board[0][0] == player && board[1][1] == player && board[2][2] == player)
            return true;
        if (board[0][2] == player && board[1][1] == player && board[2][0] == player)
            return true;

        return false;
    }
}

public class TicTacToeHardStrategy : ITicTacToeDifficultyStrategy
{
    public Level Level => Level.Hard;

    public int MakeAIMove(int[][] board)
    {
        return MakeAIMoveSmart(board);
    }

    private int MakeAIMoveSmart(int[][] board)
    {
        int bestScore = int.MinValue;
        int bestMove = -1;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i][j] == 0)
                {
                    board[i][j] = 2;
                    int score = Minimax(board, 0, false);
                    board[i][j] = 0;

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i * 3 + j;
                    }
                }
            }
        }

        return bestMove;
    }

    private int Minimax(int[][] board, int depth, bool isMaximizing)
    {
        if (CheckWin(board, 2)) return 10 - depth;
        if (CheckWin(board, 1)) return depth - 10;
        if (IsBoardFull(board)) return 0;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i][j] == 0)
                    {
                        board[i][j] = 2;
                        int score = Minimax(board, depth + 1, false);
                        board[i][j] = 0;
                        bestScore = Math.Max(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i][j] == 0)
                    {
                        board[i][j] = 1;
                        int score = Minimax(board, depth + 1, true);
                        board[i][j] = 0;
                        bestScore = Math.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }

    private bool CheckWin(int[][] board, int player)
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i][0] == player && board[i][1] == player && board[i][2] == player)
                return true;
        }

        for (int j = 0; j < 3; j++)
        {
            if (board[0][j] == player && board[1][j] == player && board[2][j] == player)
                return true;
        }

        if (board[0][0] == player && board[1][1] == player && board[2][2] == player)
            return true;
        if (board[0][2] == player && board[1][1] == player && board[2][0] == player)
            return true;

        return false;
    }

    private bool IsBoardFull(int[][] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i][j] == 0)
                    return false;
            }
        }
        return true;
    }
}