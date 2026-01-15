using QuickFun.Domain.Entities.Sudoku;
using QuickFun.Domain.Enums;
using System.Net.Http.Json;
using QuickFun.Games.Base;
namespace QuickFun.Games.Engines.Sudoku;


public class SudokuEngine : BaseGameEngine
{
    private readonly HttpClient _httpClient;
    public override string Name => "Sudoku";
    public override GameType Type => GameType.Sudoku;
    public int Score => 0;
    public int[][]? Board { get; private set; }
    public bool[][]? IsOriginal { get; private set; }
    public bool IsLoading { get; private set; }

    public SudokuEngine(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task Start(Func<Task> onStateChanged) => await LoadBoard("medium", onStateChanged);

    public async Task LoadBoard(string difficulty, Func<Task> onStateChanged)
    {
        IsLoading = true;
        await onStateChanged();

        try
        {
            var response = await _httpClient.GetFromJsonAsync<SudokuResponse>($"api/sudoku/generate?difficulty={difficulty}");
            if (response?.Board != null)
            {
                Board = response.Board;
                IsOriginal = new bool[9][];
                for (int r = 0; r < 9; r++)
                {
                    IsOriginal[r] = new bool[9];
                    for (int c = 0; c < 9; c++)
                        IsOriginal[r][c] = Board[r][c] != 0;
                }
            }
        }
        catch (Exception ex) { Console.WriteLine($"Błąd: {ex.Message}"); }
        finally
        {
            IsLoading = false;
            await onStateChanged();
        }
    }

    public void UpdateCell(int row, int col, int value)
    {
        if (Board != null && IsOriginal != null && !IsOriginal[row][col])
        {
            if (value >= 0 && value <= 9)
                Board[row][col] = value;
        }
    }

    public void Reset() => Board = null;
}