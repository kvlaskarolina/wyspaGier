using QuickFun.Games.Hangman;
using QuickFun.Games.Hangman.WordProviders;
using QuickFun.Domain.Enums;

namespace QuickFun.Tests.Games.Hangman
{
    public class HangmanEngineTests
    {
        private HangmanEngine CreateEngine(HangmanDifficulty difficulty = HangmanDifficulty.Medium)
        {
            return HangmanFactory.CreateGame(difficulty);
        }

        [Fact]
        public void HangmanEngine_InitialState_ShouldBeCorrect()
        {
            var engine = CreateEngine();

            Assert.Equal(GameType.Hangman, engine.Type);
            Assert.Equal("Hangman", engine.Name);
            Assert.Equal(0, engine.Score);
            Assert.False(engine.IsGameOver);
            Assert.Equal(0, engine.WrongAttempts);
            Assert.Equal(7, engine.MaxAttempts); 
            Assert.Empty(engine.GuessedLetters);
            Assert.Equal(string.Empty, engine.WordToGuess);
            Assert.Empty(engine.CurrentGuess);
            Assert.Equal("NotStarted", engine.GetCurrentStateName());
        }

        [Fact]
        public void Reset_ShouldInitializeGameState()
        {
            var engine = CreateEngine();

            engine.Reset();

            Assert.NotEmpty(engine.WordToGuess);
            Assert.Equal(engine.WordToGuess.Length, engine.CurrentGuess.Length);
            Assert.All(engine.CurrentGuess, c => Assert.Equal('_', c));
            Assert.Equal(0, engine.WrongAttempts);
            Assert.False(engine.IsGameOver);
            Assert.Equal(0, engine.Score);
            Assert.Empty(engine.GuessedLetters);
            Assert.Contains("Let's start", engine.Message);
            Assert.Equal("Playing", engine.GetCurrentStateName());
        }

        [Fact]
        public void MakeMove_CorrectLetter_ShouldRevealLetter()
        {
            var engine = CreateEngine();
            engine.Reset();
            var firstLetter = engine.WordToGuess[0];

            engine.MakeMove(firstLetter);

            Assert.Contains(firstLetter, engine.GuessedLetters);
            Assert.Contains(firstLetter, engine.CurrentGuess);
            Assert.Equal(0, engine.WrongAttempts);
            Assert.False(engine.IsGameOver);
            Assert.Contains("Nice! Letter found:", engine.Message);
            Assert.Equal("Playing", engine.GetCurrentStateName());
        }

        [Fact]
        public void MakeMove_WrongLetter_ShouldIncreaseWrongAttempts()
        {
            var engine = CreateEngine();
            engine.Reset();
            char wrongLetter = 'z';
            while (engine.WordToGuess.Contains(wrongLetter))
            {
                wrongLetter = (char)(wrongLetter - 1);
            }

            engine.MakeMove(wrongLetter);

            Assert.Contains(wrongLetter, engine.GuessedLetters);
            Assert.Equal(1, engine.WrongAttempts);
            Assert.False(engine.IsGameOver);
            Assert.Contains("Oops! You have", engine.Message);
            Assert.Contains("tries left!", engine.Message);
            Assert.Equal("Playing", engine.GetCurrentStateName());
        }

        [Fact]
        public void MakeMove_DuplicateLetter_ShouldBeIgnored()
        {
            var engine = CreateEngine();
            engine.Reset();
            var letter = engine.WordToGuess[0];
            engine.MakeMove(letter);
            var attemptsBefore = engine.WrongAttempts;

            engine.MakeMove(letter);

            Assert.Equal(attemptsBefore, engine.WrongAttempts);
            Assert.Single(engine.GuessedLetters);
        }

        [Fact]
        public void MakeMove_GuessAllLetters_ShouldWinGame()
        {
            var engine = CreateEngine();
            engine.Reset();
            var uniqueLetters = engine.WordToGuess.Distinct().ToArray();

            foreach (var letter in uniqueLetters)
            {
                engine.MakeMove(letter);
            }

            Assert.True(engine.IsGameOver);
            Assert.Equal(1, engine.Score);
            Assert.DoesNotContain('_', engine.CurrentGuess);
            Assert.Contains("Bravo! You guessed the word", engine.Message);
            Assert.Equal("Won", engine.GetCurrentStateName());
        }

        [Fact]
        public void MakeMove_SevenWrongAttempts_ShouldLoseGame()
        {
            var engine = CreateEngine(HangmanDifficulty.Medium);
            engine.Reset();

            var allLetters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var wrongLetters = allLetters.Where(l => !engine.WordToGuess.Contains(l)).Take(7).ToArray();

            foreach (var letter in wrongLetters)
            {
                engine.MakeMove(letter);
            }

            Assert.True(engine.IsGameOver);
            Assert.Equal(0, engine.Score);
            Assert.Equal(7, engine.WrongAttempts);
            Assert.Contains("You lost! The word was:", engine.Message);
            Assert.Contains(engine.WordToGuess, engine.Message);
            Assert.Equal("Lost", engine.GetCurrentStateName());
        }

        [Fact]
        public void MakeMove_AfterGameOver_ShouldBeIgnored()
        {
            var engine = CreateEngine();
            engine.Reset();
            var uniqueLetters = engine.WordToGuess.Distinct().ToArray();
            foreach (var letter in uniqueLetters)
            {
                engine.MakeMove(letter);
            }
            var guessedCountBefore = engine.GuessedLetters.Count;

            engine.MakeMove('a');

            Assert.Equal(guessedCountBefore, engine.GuessedLetters.Count);
            Assert.True(engine.IsGameOver);
        }

        [Fact]
        public void HangmanPicture_ShouldProgressWithWrongAttempts()
        {
            var engine = CreateEngine(HangmanDifficulty.Easy);
            engine.Reset();
            var pictures = new List<string>();

            for (int i = 0; i <= 7; i++)
            {
                var picture = engine.HangmanPicture;
                pictures.Add(picture);

                if (i < 7)
                {
                    char wrongLetter = (char)('z' - i);
                    while (engine.WordToGuess.Contains(wrongLetter))
                    {
                        wrongLetter = (char)(wrongLetter - 1);
                    }
                    engine.MakeMove(wrongLetter);
                }
            }
            Assert.Equal(8, pictures.Distinct().Count());
            Assert.Contains("/ \\", pictures[7]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void HangmanPicture_ShouldReturnValidAsciiArt(int wrongAttempts)
        {
            var engine = CreateEngine();
            engine.Reset();

            for (int i = 0; i < wrongAttempts; i++)
            {
                char wrongLetter = (char)('z' - i);
                while (engine.WordToGuess.Contains(wrongLetter))
                {
                    wrongLetter = (char)(wrongLetter - 1);
                }
                engine.MakeMove(wrongLetter);
            }

            var picture = engine.HangmanPicture;

            Assert.NotEmpty(picture);
            Assert.Contains("+---+", picture);
            Assert.Contains("=========", picture);
        }

        [Fact]
        public void Reset_AfterGameOver_ShouldStartNewGame()
        {
            var engine = CreateEngine();
            engine.Reset();
            var uniqueLetters = engine.WordToGuess.Distinct().ToArray();
            foreach (var letter in uniqueLetters)
            {
                engine.MakeMove(letter);
            }
            Assert.True(engine.IsGameOver);

            engine.Reset();

            Assert.False(engine.IsGameOver);
            Assert.Equal(0, engine.WrongAttempts);
            Assert.Equal(0, engine.Score);
            Assert.Empty(engine.GuessedLetters);
            Assert.All(engine.CurrentGuess, c => Assert.Equal('_', c));
            Assert.Equal("Playing", engine.GetCurrentStateName());
        }


        [Fact]
        public void Factory_ShouldCreateEasyGame()
        {
            var engine = HangmanFactory.CreateGame(HangmanDifficulty.Easy);

            Assert.Equal(HangmanDifficulty.Easy, engine.Difficulty);
            Assert.Equal(10, engine.MaxAttempts);
        }

        [Fact]
        public void Factory_ShouldCreateMediumGame()
        {
            var engine = HangmanFactory.CreateGame(HangmanDifficulty.Medium);

            Assert.Equal(HangmanDifficulty.Medium, engine.Difficulty);
            Assert.Equal(7, engine.MaxAttempts);
        }

        [Fact]
        public void Factory_ShouldCreateHardGame()
        {
            var engine = HangmanFactory.CreateGame(HangmanDifficulty.Hard);

            Assert.Equal(HangmanDifficulty.Hard, engine.Difficulty);
            Assert.Equal(5, engine.MaxAttempts);
        }

        [Fact]
        public void State_ShouldTransitionFromNotStartedToPlaying()
        {
            var engine = CreateEngine();
            Assert.Equal("NotStarted", engine.GetCurrentStateName());

            engine.Reset();

            Assert.Equal("Playing", engine.GetCurrentStateName());
        }

        [Fact]
        public void State_ShouldTransitionFromPlayingToWon()
        {
            var engine = CreateEngine();
            engine.Reset();
            Assert.Equal("Playing", engine.GetCurrentStateName());

            var uniqueLetters = engine.WordToGuess.Distinct().ToArray();
            foreach (var letter in uniqueLetters)
            {
                engine.MakeMove(letter);
            }

            Assert.Equal("Won", engine.GetCurrentStateName());
        }

        [Fact]
        public void State_ShouldTransitionFromPlayingToLost()
        {
            var engine = CreateEngine();
            engine.Reset();
            Assert.Equal("Playing", engine.GetCurrentStateName());

            var allLetters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var wrongLetters = allLetters.Where(l => !engine.WordToGuess.Contains(l)).Take(7).ToArray();

            foreach (var letter in wrongLetters)
            {
                engine.MakeMove(letter);
            }

            Assert.Equal(7, engine.WrongAttempts);
            Assert.Equal("Lost", engine.GetCurrentStateName());
        }

        [Fact]
        public void Strategy_EasyWords_ShouldBeShorter()
        {
            var engine = CreateEngine(HangmanDifficulty.Easy);
            engine.Reset();

            Assert.True(engine.WordToGuess.Length <= 5);
        }

        [Fact]
        public void Strategy_HardWords_ShouldBeLonger()
        {
            var engine = CreateEngine(HangmanDifficulty.Hard);
            engine.Reset();

            Assert.True(engine.WordToGuess.Length >= 8);
        }
    }
}
