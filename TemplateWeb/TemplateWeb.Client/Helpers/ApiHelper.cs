using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace TemplateWeb.Client.Helpers;

public class ApiHelper
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;

    public ApiHelper(
        HttpClient httpClient,
        NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
    }

    public async Task<T> GetAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dataJson = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<T>(dataJson, options);
            return data;
        }
        else
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("login");
            }
            // TODO: Do generic error handling
            return default;
        }
    }
}