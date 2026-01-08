namespace QuickFun.Games.Hangman.States
{
    public class NotStartedState : IHangmanState
    {
        public string StateName => "NotStarted";
        public bool CanMakeMove => false;
        public bool IsGameOver => false;

        public IHangmanState HandleMove(HangmanEngine context, char letter) {
            return this;
        }

        public string GetMessage(HangmanEngine context)
        {
            return "Press Reset to start the game!";
        }
    }
}
