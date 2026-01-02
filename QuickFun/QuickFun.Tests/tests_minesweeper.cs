using Xunit;
using QuickFun.Games.Minesweeper;
using QuickFun.Games.Minesweeper.Strategies;
using FluentAssertions.Equivalency.Steps;
using Moq;

namespace QuickFun.Tests.Unit.Games;

public class MinesweeperEngineTests
{
    //mock
    private class MockFloodingStrategy : IMinesweeperFloodingStrategy
    {
        public void flood(MinesweeperCell[,] Board, int r, int c, int rows, int cols)
        {
            Board[r, c].IsRevealed = true;
        }
    }

    [Fact]
    public void IsThere144Cells()
    {
        var engine = new MinesweeperEngine(new MockFloodingStrategy());

        // Assert
        Assert.Equal(144, engine.Board.Cast<MinesweeperCell>().Count());
    }
    
    [Fact]
    public void AllCellsHidden()
    {
        var engine = new MinesweeperEngine(new MockFloodingStrategy());
        int rows = 12;
        int cols = 12;

        // Assert
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Assert.False(engine.Board[r, c].IsRevealed, $"Pole [{r},{c}] powinno być ukryte");
            }
        }
    }

    [Fact]
    public void IsScoreZero()
    {
        var engine = new MinesweeperEngine(new MockFloodingStrategy());

        // Assert
        Assert.True(engine.Score == 0, "Score powinno byc 0");
    }

    [Fact]
    public void MineCount_ShouldBe20()
    {
        // Arrange
        var engine = new MinesweeperEngine(new MockFloodingStrategy());

        // Act
        engine.HandleClick(0, 0);
        int mineCount = engine.Board.Cast<MinesweeperCell>().Count(x => x.IsMine);

        // Assert
        Assert.Equal(20, mineCount);
    }

    [Fact]
    public void HandleClick_FirstClick55()
    {
        // Arrange
        var engine = new MinesweeperEngine(new MockFloodingStrategy());
        int clickR = 5, clickC = 5;

        // Act
        engine.HandleClick(clickR, clickC);

        // Assert
        for (int r = clickR - 1; r <= clickR + 1; r++)
        {
            for (int c = clickC - 1; c <= clickC + 1; c++)
            {
                Assert.False(engine.Board[r, c].IsMine, $"Pole [{r},{c}] powinno byc bezpieczne");
            }
        }
    }

    [Fact]
    public void HandleClick_FirstClick00()
    {
        // Arrange
        var engine = new MinesweeperEngine(new MockFloodingStrategy());
        int clickR = 0, clickC = 0;

        // Act
        engine.HandleClick(clickR, clickC);

        // Assert
        for (int r = clickR - 1; r <= clickR + 1; r++)
        {
            if (r < 0 || r >= 12) continue;
            for (int c = clickC - 1; c <= clickC + 1; c++)
            {
                if (c < 0 || c >= 12) continue;
                Assert.False(engine.Board[r, c].IsMine, $"Pole [{r},{c}] powinno byc bezpieczne");
            }
        }
    }

    [Fact]
    public void HandleClick_AlreadyClicked()
    {
        // Arrange
        var engine = new MinesweeperEngine(new MockFloodingStrategy());
        engine.HandleClick(0, 0);
        var initialScore = engine.Score;

        // Act
        engine.HandleClick(0, 0);

        // Assert
        Assert.Equal(initialScore, engine.Score);
    }

    [Fact]
    public void SwitchFlag()
    {
        // Arrange
        var engine = new MinesweeperEngine(new MockFloodingStrategy());
        engine.HandleClick(0, 0);
        engine.SwitchFlag(1, 1);

        // Act
        engine.HandleClick(1, 1);

        // Assert
        Assert.False(engine.Board[1, 1].IsRevealed);
    }

    [Fact]
    public void GameOver_RevealsAllMines()
    {
        // Arrange
        var engine = new MinesweeperEngine(new MockFloodingStrategy());
        engine.HandleClick(0, 0);

        var mine = engine.Board.Cast<MinesweeperCell>().First(x => x.IsMine);

        // Act
        engine.HandleClick(mine.R, mine.C);

        // Assert
        Assert.True(engine.IsGameOver);
        var allMinesRevealed = engine.Board.Cast<MinesweeperCell>()
            .Where(x => x.IsMine)
            .All(x => x.IsRevealed);
        
        Assert.True(allMinesRevealed);
    }

    [Fact]
    public void OnStateChanged_IsTriggeredOnClick()
    {
        // Arrange
        var engine = new MinesweeperEngine(new MockFloodingStrategy());
        bool wasCalled = false;
        engine.OnStateChanged += () => wasCalled = true;

        // Act
        engine.HandleClick(0, 0);

        // Assert
        Assert.True(wasCalled);
    }

    [Fact]
    public void AdjMines_Calculation()
    {
        // Arrange
        var engine = new MinesweeperEngine(new MockFloodingStrategy());
        engine.HandleClick(0,0);

        var cellWithNeighborMine = engine.Board.Cast<MinesweeperCell>()
            .First(x => !x.IsMine && x.AdjMines > 0);

        int manualCount = 0;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
            {
                int ni = cellWithNeighborMine.R + i;
                int nj = cellWithNeighborMine.C + j;
                if (ni >= 0 && ni < 12 && nj >= 0 && nj < 12 && engine.Board[ni, nj].IsMine)
                    manualCount++;
            }

        // Assert
        Assert.Equal(manualCount, cellWithNeighborMine.AdjMines);
    }

    [Fact]
    public void CheckWin()
    {
        // Arrange
        var engine = new MinesweeperEngine(new MockFloodingStrategy());
        engine.HandleClick(0, 0);

        // Act
        foreach (var cell in engine.Board)
        {
            if (!cell.IsMine && !cell.IsRevealed)
            {
                engine.HandleClick(cell.R, cell.C);
            }
        }

        // Assert
        Assert.True(engine.IsGameOver);
        Assert.True(engine.IsWin);
        Assert.Equal("Mega big win!!!", engine.Message);
    }

    [Fact]
    public void Reset_RestoresInitialState()
    {
        // Arrange
        var engine = new MinesweeperEngine(new MockFloodingStrategy());
        engine.HandleClick(0, 0);
        var mine = engine.Board.Cast<MinesweeperCell>().First(x => x.IsMine);
        engine.HandleClick(mine.R, mine.C);

        // Act
        engine.Reset();

        // Assert
        Assert.False(engine.IsGameOver);
        Assert.Equal(0, engine.Score);
        Assert.Equal("Powodzenia!", engine.Message);
        Assert.All(engine.Board.Cast<MinesweeperCell>(), c => Assert.False(c.IsRevealed));
        Assert.All(engine.Board.Cast<MinesweeperCell>(), c => Assert.False(c.IsMine));
    }
}