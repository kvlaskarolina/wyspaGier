using QuickFun.Games.Engines;
using QuickFun.Domain.Enums;

namespace QuickFun.Tests.Games.TicTacToe
{
    public class tests_tictactoe
    {
        [Fact]
        public void TicTacToeEngine_InitialState_ShouldBeCorrect()
        {
            var engine = new TicTacToeEngine();

            Assert.Equal(GameType.TicTacToe, engine.Type);
            Assert.Equal("Kółko i Krzyżyk", engine.Name);
            Assert.Equal(0, engine.Score);
            Assert.False(engine.IsGameOver);
            Assert.Equal('X', engine.CurrentPlayer);
            Assert.Equal("Zaczyna X", engine.Message);
            Assert.All(engine.Board, cell => Assert.Equal('\0', cell));
        }
    }
}
