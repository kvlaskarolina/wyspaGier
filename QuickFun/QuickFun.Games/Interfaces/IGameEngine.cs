using QuickFun.Domain.Enums;

namespace QuickFun.Games
{
    public interface IGameEngine
    {
        GameType Type { get; }
        string Name { get; }
        int Score { get; }
        void Reset();
    }
}