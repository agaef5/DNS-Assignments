using System.Text.Json;
using DTOs.Posts;

namespace ClientApp.Services.Post;

public class HttpPostService(HttpClient client) : IPostService
{
    public async Task<PostDto> CreatePostAsync(CreatePostDto postDto)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("posts", postDto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if(!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<PostDto>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task<List<PostDto>> GetAllPostsAsync()
    {
        HttpResponseMessage httpResponse =
            await client.GetAsync("posts");
        string response = await httpResponse.Content.ReadAsStringAsync();
        
        if(!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        };
        
        return JsonSerializer.Deserialize<List<PostDto>>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task<PostDto> GetPostByIdAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await client.GetAsync($"posts/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        
        if(!httpResponse.IsSuccessStatusCode){
            throw new Exception(response);
        };
        
        return JsonSerializer.Deserialize<PostDto>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }
}