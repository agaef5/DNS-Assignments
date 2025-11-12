using System.Text.Json;
using DTOs;
using DTOs.Users;

namespace ClientApp.Services.User;

public class HttpUserService(HttpClient client) : IUserService
{
    public async Task<UserDto> AddUserAsync(AuthUserDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) {
            throw new Exception(response);
        } 
        return JsonSerializer.Deserialize<UserDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    } 
}