using System.Net.Http.Json;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System;
using Template.Models.Models;

namespace TemplateWeb.Client.Services;

public class JwtAuthService
{
    public static string Token { get; set; }
    private readonly HttpClient _client;
    private readonly IJSRuntime _jsRuntime;

    public JwtAuthService(HttpClient client, IJSRuntime jsRuntime)
    {
        _client = client;
        _jsRuntime = jsRuntime;
    }
    
    public async Task<bool> InitializeTokenAsync()
    {
        Token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwtToken");
        return IsAuthenticated;
    }

    public bool IsAuthenticated 
    { 
        get
        {
            if(!string.IsNullOrEmpty(Token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(Token);
                if (jwtToken.ValidTo > DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        } 
    }
    
    public async Task<bool> Authenticate(LoginModel loginModel)
    {
        var generateTokenResponse = await _client.PostAsJsonAsync("/api/token", loginModel);
        if (generateTokenResponse.IsSuccessStatusCode)
        {
            Token = await generateTokenResponse.Content.ReadAsStringAsync();
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwtToken", Token);

            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task Logout()
    {
        Token = null;
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "jwtToken");
    }
}