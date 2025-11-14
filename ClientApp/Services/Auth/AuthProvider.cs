using System.Security.Claims;
using System.Text.Json;
using DTOs.Users;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClientApp.Services.Auth;

public class AuthProvider(HttpClient client) : AuthenticationStateProvider
{
    private ClaimsPrincipal currentClaimsPrincipal;
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(currentClaimsPrincipal ?? new());
    }

    public async Task Register(string username, string password)
    {
        HttpResponseMessage responseMessage =
            await client.PostAsJsonAsync("Auth/register",
                new LoginRequest { Username = username, Password = password });
        
        string content = await responseMessage.Content.ReadAsStringAsync();
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        await Login(username, password);
    }
    
    public async Task Login(string username, string password)
    {
        HttpResponseMessage responseMessage =
            await client.PostAsJsonAsync("Auth/login",
                new LoginRequest { Username = username, Password = password });

        string content = await responseMessage.Content.ReadAsStringAsync();

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        UserDto userDto = JsonSerializer.Deserialize<UserDto>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim("Id", userDto.UserId.ToString())
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        currentClaimsPrincipal = new ClaimsPrincipal(identity);
        
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(currentClaimsPrincipal))
            );
    }

    public void Logout()
    {
        currentClaimsPrincipal = new();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentClaimsPrincipal)));
    }
}