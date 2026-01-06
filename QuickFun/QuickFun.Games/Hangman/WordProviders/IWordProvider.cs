namespace QuickFun.Games.Hangman.WordProviders
{
    public interface IWordProvider
    {
        string GetRandomWord();
        
        HangmanDifficulty Difficulty { get; }
    }
}
