namespace QuickFun.Games.Hangman.States
{
    public interface IHangmanState
    {
        string StateName { get; }
        
        bool CanMakeMove { get; }
        
        bool IsGameOver { get; }
        
        IHangmanState HandleMove(HangmanEngine context, char letter);

        string GetMessage(HangmanEngine context);
    }
}
