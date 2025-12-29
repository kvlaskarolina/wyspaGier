using System;
using System.Linq;
using QuickFun.Domain.Enums;
namespace QuickFun.Games.Memory;

public class MemoryCard
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsRevealed { get; set; } = false;
    public bool IsMatched { get; set; } = false;
}

public class MemoryEngine : IGameEngine
{
    public GameType Type => GameType.Memory;
    public string Name => "Memory";
    public int Score { get; private set; } = 0;
    public List<MemoryCard> Cards { get; private set; } = new();
    public string Message { get; private set; } = "jeszcze nie wiem jaki komunikat";

    public MemoryEngine()
    {
        Reset();
    }

    public void Reset()
    {
        Score = 0;
        Message = "Znajdź pary obrazków";
        InitializeGame();
    }

    public void InitializeGame()
    {
        var paths = new List<string>
        {
            "/images/imbir.png", "/images/jablka.png", "/images/lody.png", "/images/nos.png",
            "/images/pies.png", "/images/piwo.png", "/images/superman.png", "/images/truskawka.png"
        };

        var pathsPairs = paths.Concat(paths).ToList();

        var random = new Random();
        var shuffled = pathsPairs.OrderBy(x => Guid.NewGuid()).ToList();

        this.Cards = shuffled.Select((path, index) => new MemoryCard
        {
            Id = index,
            ImageUrl = path
        }).ToList();
    }


    private MemoryCard? _firstCard;
    private MemoryCard? _secondCard;
    private bool _isBusy = false;

    public async Task Start(Func<Task> onStateChanged)
    {
        Reset();

        foreach (var card in Cards)
            card.IsRevealed = true;

        Message = "Zapamiętaj obrazki!";
        await onStateChanged();

        await Task.Delay(3000);

        foreach (var card in Cards)
            card.IsRevealed = false;

        Message = "Zaczynamy! Szukaj par.";
        await onStateChanged();
    }

    public async Task HandleCardClick(MemoryCard clicked)
    {
        if (_isBusy || clicked.IsRevealed || clicked.IsMatched) return;
        clicked.IsRevealed = true;
        if (_firstCard == null)
        {
            _firstCard = clicked;
            Message = "Wybierz drugą kartę";
        }
        else
        {
            _secondCard = clicked;
            _isBusy = true;
            await CheckMatch();
        }
    }

    private async Task CheckMatch()
    {
        if (_firstCard!.ImageUrl == _secondCard!.ImageUrl)
        {
            _firstCard.IsMatched = true;
            _secondCard.IsMatched = true;
            Score += 1;
            Message = "Znaleziono parę.";
        }
        else
        {
            Message = "Nie pasują!";
            await Task.Delay(1000);
            _firstCard.IsRevealed = false;
            _secondCard.IsRevealed = false;
        }

        _firstCard = null;
        _secondCard = null;
        _isBusy = false;

        CheckWin();
    }

    public bool IsGameOver { get; private set; } = false;

    private void CheckWin()
    {
        if (Cards.All (card => card.IsMatched))
        {
            IsGameOver = true;
            Message = $"Gratulacje! Wygrałeś z wynikiem {Score}!";
        }
    }
}