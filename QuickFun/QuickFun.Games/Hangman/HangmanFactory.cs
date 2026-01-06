using QuickFun.Games.Hangman.WordProviders;

namespace QuickFun.Games.Hangman
{
    public static class HangmanFactory
    {
        public static HangmanEngine CreateGame(HangmanDifficulty difficulty = HangmanDifficulty.Medium)
        {
            IWordProvider wordProvider = difficulty switch
            {
                HangmanDifficulty.Easy => new EasyWordProvider(),
                HangmanDifficulty.Medium => new MediumWordProvider(),
                HangmanDifficulty.Hard => new HardWordProvider(),
                _ => new MediumWordProvider()
            };

            return new HangmanEngine(wordProvider, difficulty);
        }
    }
}