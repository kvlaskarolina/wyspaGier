using QuickFun.Games.Engines;
using System.Net.Http.Json;
using QuickFun.Domain.Enums;
using QuickFun.Domain.Entities;
using QuickFun.Domain.Entities.Sudoku;


namespace QuickFun.Games.Engines.Sudoku;

public class SudokuEngine : IGameEngine
{
    private readonly HttpClient _httpClient;
    public GameType Type => GameType.Sudoku;
    public string Name => "Sudoku";
    public int Score => 0;
    public int[][]? Board { get; private set; }
    public bool IsLoading { get; private set; }

    public SudokuEngine(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task Start(Func<Task> onStateChanged)
    {
        IsLoading = true;
        await onStateChanged();

        try
        {
            var response = await _httpClient.GetFromJsonAsync<SudokuResponse>("http://localhost:3000/api/sudoku/generate?difficulty=medium");
            Board = response?.Board;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd pobierania Sudoku: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
            await onStateChanged();
        }
    }

    public void Reset()
    {
        Board = null;
    }
}