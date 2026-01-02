namespace QuickFun.Games.Hangman
{
    public static class HangmanFactory
    {
        public static HangmanEngine CreateGame()
        {
            return new HangmanEngine();
        }
    }
}