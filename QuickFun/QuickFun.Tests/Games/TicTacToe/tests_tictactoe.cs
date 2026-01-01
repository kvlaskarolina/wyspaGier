using Xunit;
using QuickFun.Games.Engines;
using QuickFun.Domain.Enums;

namespace QuickFun.Tests.Unit.Games;

public class TicTacToeEngineTests
{
    [Fact]
    public void Constructor_InitializesEmptyBoard()
    {
        // Arrange & Act
        var engine = new TicTacToeEngine();

        // Assert
        Assert.Equal(9, engine.Board.Length);
        Assert.All(engine.Board, cell => Assert.Equal('\0', cell));
    }

    [Fact]
    public void Constructor_SetsInitialPlayer_ToX()
    {
        // Arrange & Act
        var engine = new TicTacToeEngine();

        // Assert
        Assert.Equal('X', engine.CurrentPlayer);
    }

    [Fact]
    public void Constructor_SetsGameNotOver()
    {
        // Arrange & Act
        var engine = new TicTacToeEngine();

        // Assert
        Assert.False(engine.IsGameOver);
    }

    [Fact]
    public void Constructor_SetsInitialScore_ToZero()
    {
        // Arrange & Act
        var engine = new TicTacToeEngine();

        // Assert
        Assert.Equal(0, engine.Score);
    }

    [Fact]
    public void Constructor_SetsInitialMessage()
    {
        // Arrange & Act
        var engine = new TicTacToeEngine();

        // Assert
        Assert.Equal("Zaczyna X", engine.Message);
    }

    [Fact]
    public void Type_ReturnsCorrectGameType()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act & Assert
        Assert.Equal(GameType.TicTacToe, engine.Type);
    }

    [Fact]
    public void Name_ReturnsCorrectGameName()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act & Assert
        Assert.Equal("Kółko i Krzyżyk", engine.Name);
    }

    [Fact]
    public void Reset_ClearsBoard()
    {
        // Arrange
        var engine = new TicTacToeEngine();
        engine.MakeMove(0);
        engine.MakeMove(1);

        // Act
        engine.Reset();

        // Assert
        Assert.All(engine.Board, cell => Assert.Equal('\0', cell));
    }

    [Fact]
    public void Reset_ResetsCurrentPlayer_ToX()
    {
        // Arrange
        var engine = new TicTacToeEngine();
        engine.MakeMove(0); // X
        engine.MakeMove(1); // O

        // Act
        engine.Reset();

        // Assert
        Assert.Equal('X', engine.CurrentPlayer);
    }

    [Fact]
    public void Reset_SetsGameNotOver()
    {
        // Arrange
        var engine = new TicTacToeEngine();
        engine.MakeMove(0); // X
        engine.MakeMove(3); // O
        engine.MakeMove(1); // X
        engine.MakeMove(4); // O
        engine.MakeMove(2); // X wins

        // Act
        engine.Reset();

        // Assert
        Assert.False(engine.IsGameOver);
    }

    [Fact]
    public void Reset_ResetsScore()
    {
        // Arrange
        var engine = new TicTacToeEngine();
        engine.MakeMove(0);
        engine.MakeMove(3);
        engine.MakeMove(1);
        engine.MakeMove(4);
        engine.MakeMove(2); // X wins, score = 1

        // Act
        engine.Reset();

        // Assert
        Assert.Equal(0, engine.Score);
    }

    [Fact]
    public void MakeMove_PlacesCorrectSymbol_OnBoard()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act
        engine.MakeMove(0);

        // Assert
        Assert.Equal('X', engine.Board[0]);
    }

    [Fact]
    public void MakeMove_SwitchesPlayer_AfterMove()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act
        engine.MakeMove(0);

        // Assert
        Assert.Equal('O', engine.CurrentPlayer);
    }

    [Fact]
    public void MakeMove_UpdatesMessage_AfterMove()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act
        engine.MakeMove(0);

        // Assert
        Assert.Equal("Ruch gracza: O", engine.Message);
    }

    [Fact]
    public void MakeMove_IgnoresMove_OnOccupiedCell()
    {
        // Arrange
        var engine = new TicTacToeEngine();
        engine.MakeMove(0); // X
        var playerBeforeSecondMove = engine.CurrentPlayer;

        // Act
        engine.MakeMove(0); // Try to place O on same cell

        // Assert
        Assert.Equal('X', engine.Board[0]); // Still X
        Assert.Equal(playerBeforeSecondMove, engine.CurrentPlayer); // Player didn't change
    }

    [Fact]
    public void MakeMove_IgnoresMove_WhenGameIsOver()
    {
        // Arrange
        var engine = new TicTacToeEngine();
        engine.MakeMove(0); // X
        engine.MakeMove(3); // O
        engine.MakeMove(1); // X
        engine.MakeMove(4); // O
        engine.MakeMove(2); // X wins

        // Act
        engine.MakeMove(5); // Try to make move after game over

        // Assert
        Assert.Equal('\0', engine.Board[5]); // Cell still empty
    }

    [Theory]
    [InlineData(0, 1, 2)] // Top row
    [InlineData(3, 4, 5)] // Middle row
    [InlineData(6, 7, 8)] // Bottom row
    public void MakeMove_DetectsWin_InRows(int pos1, int pos2, int pos3)
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act
        engine.MakeMove(pos1); // X
        engine.MakeMove((pos1 + 3) % 9); // O somewhere else
        engine.MakeMove(pos2); // X
        engine.MakeMove((pos1 + 4) % 9); // O somewhere else
        engine.MakeMove(pos3); // X wins

        // Assert
        Assert.True(engine.IsGameOver);
        Assert.Contains("Wygrał gracz: X", engine.Message);
        Assert.Equal(1, engine.Score);
    }

    [Theory]
    [InlineData(0, 3, 6)] // Left column
    [InlineData(1, 4, 7)] // Middle column
    [InlineData(2, 5, 8)] // Right column
    public void MakeMove_DetectsWin_InColumns(int pos1, int pos2, int pos3)
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act
        engine.MakeMove(pos1); // X
        engine.MakeMove((pos1 + 1) % 9); // O somewhere else
        engine.MakeMove(pos2); // X
        engine.MakeMove((pos1 + 2) % 9); // O somewhere else
        engine.MakeMove(pos3); // X wins

        // Assert
        Assert.True(engine.IsGameOver);
        Assert.Contains("Wygrał gracz: X", engine.Message);
        Assert.Equal(1, engine.Score);
    }

    [Fact]
    public void MakeMove_DetectsWin_InMainDiagonal()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act
        engine.MakeMove(0); // X
        engine.MakeMove(1); // O
        engine.MakeMove(4); // X
        engine.MakeMove(2); // O
        engine.MakeMove(8); // X wins (0, 4, 8)

        // Assert
        Assert.True(engine.IsGameOver);
        Assert.Contains("Wygrał gracz: X", engine.Message);
        Assert.Equal(1, engine.Score);
    }

    [Fact]
    public void MakeMove_DetectsWin_InAntiDiagonal()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act
        engine.MakeMove(2); // X
        engine.MakeMove(0); // O
        engine.MakeMove(4); // X
        engine.MakeMove(1); // O
        engine.MakeMove(6); // X wins (2, 4, 6)

        // Assert
        Assert.True(engine.IsGameOver);
        Assert.Contains("Wygrał gracz: X", engine.Message);
        Assert.Equal(1, engine.Score);
    }

    [Fact]
    public void MakeMove_DetectsDraw_WhenBoardIsFull()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act - Create a draw scenario
        engine.MakeMove(0); // X
        engine.MakeMove(1); // O
        engine.MakeMove(2); // X
        engine.MakeMove(4); // O
        engine.MakeMove(3); // X
        engine.MakeMove(5); // O
        engine.MakeMove(7); // X
        engine.MakeMove(6); // O
        engine.MakeMove(8); // X - Draw

        // Assert
        Assert.True(engine.IsGameOver);
        Assert.Equal("Remis!", engine.Message);
        Assert.Equal(0, engine.Score);
    }

    [Fact]
    public void MakeMove_PlayerO_CanWin()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act
        engine.MakeMove(0); // X
        engine.MakeMove(3); // O
        engine.MakeMove(1); // X
        engine.MakeMove(4); // O
        engine.MakeMove(8); // X
        engine.MakeMove(5); // O wins

        // Assert
        Assert.True(engine.IsGameOver);
        Assert.Contains("Wygrał gracz: O", engine.Message);
        Assert.Equal(1, engine.Score);
    }

    [Fact]
    public void MakeMove_AllPositions_AreAccessible()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act & Assert - Test all 9 positions
        for (int i = 0; i < 9; i++)
        {
            engine.Reset();
            engine.MakeMove(i);
            Assert.NotEqual('\0', engine.Board[i]);
        }
    }

    [Fact]
    public void Board_HasCorrectLength()
    {
        // Arrange & Act
        var engine = new TicTacToeEngine();

        // Assert
        Assert.Equal(9, engine.Board.Length);
    }

    [Fact]
    public void MakeMove_AlternatesPlayers_Correctly()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act & Assert
        Assert.Equal('X', engine.CurrentPlayer);

        engine.MakeMove(0);
        Assert.Equal('O', engine.CurrentPlayer);

        engine.MakeMove(1);
        Assert.Equal('X', engine.CurrentPlayer);

        engine.MakeMove(2);
        Assert.Equal('O', engine.CurrentPlayer);
    }

    [Fact]
    public void CheckWinner_ReturnsFalse_OnEmptyBoard()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act
        engine.MakeMove(0);
        engine.Reset();

        // Assert - No winner on empty board
        Assert.False(engine.IsGameOver);
    }

    [Fact]
    public void Score_IncreasesOnly_OnWin()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act - Make moves without winning
        engine.MakeMove(0); // X
        engine.MakeMove(1); // O
        engine.MakeMove(3); // X

        // Assert
        Assert.Equal(0, engine.Score); // Score still 0 (no winner yet)
    }

    [Fact]
    public void MultipleGames_CanBePlayedAfterReset()
    {
        // Arrange
        var engine = new TicTacToeEngine();

        // Act - Play first game
        engine.MakeMove(0);
        engine.MakeMove(3);
        engine.MakeMove(1);
        engine.MakeMove(4);
        engine.MakeMove(2); // X wins

        Assert.True(engine.IsGameOver);

        // Reset and play second game
        engine.Reset();
        engine.MakeMove(4);
        engine.MakeMove(0);

        // Assert - New game works correctly
        Assert.False(engine.IsGameOver);
        Assert.Equal('X', engine.CurrentPlayer);
        Assert.Equal('X', engine.Board[4]);
        Assert.Equal('O', engine.Board[0]);
    }
}