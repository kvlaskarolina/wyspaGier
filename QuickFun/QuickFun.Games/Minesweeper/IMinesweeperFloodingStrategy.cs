namespace QuickFun.Games.Minesweeper.Strategies;

public interface IMinesweeperFloodingStrategy
{
    void flood (MinesweeperCell[,] Board, int r, int c, int rows, int cols);
}

public class DfsFloodingStrategy : IMinesweeperFloodingStrategy
{
    public void flood(MinesweeperCell[,] Board, int r, int c, int rows, int cols)
    {
        if (r < 0 || c < 0 || r >= rows || c >= cols) return;

        var cell = Board[r, c];
        if (cell.IsRevealed || cell.IsFlagged || cell.IsMine) return;

        cell.IsRevealed = true;
        if (cell.AdjMines == 0)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    flood(Board, r + i, c + j, rows, cols);
                }
            }
        }
    }
}
