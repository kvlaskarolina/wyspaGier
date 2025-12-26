using Microsoft.JSInterop;
using System.Text.Json;

namespace QuickFun.Infrastructure.Services;

public class LocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<T?> GetItemAsync<T>(string key)
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (string.IsNullOrEmpty(json))
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting item from localStorage: {ex.Message}");
            return default;
        }
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        try
        {
            var json = JsonSerializer.Serialize(value);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting item in localStorage: {ex.Message}");
        }
    }

    public async Task RemoveItemAsync(string key)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error removing item from localStorage: {ex.Message}");
        }
    }

    public async Task ClearAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error clearing localStorage: {ex.Message}");
        }
    }

    public async Task<bool> ContainsKeyAsync(string key)
    {
        try
        {
            var value = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            return !string.IsNullOrEmpty(value);
        }
        catch
        {
            return false;
        }
    }
}