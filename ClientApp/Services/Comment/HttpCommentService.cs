using System.Text.Json;
using DTOs.Comments;

namespace ClientApp.Services.Comment;

public class HttpCommentService(HttpClient client) : ICommentService
{
    public async Task<CommentDto> CreateCommentAsync(CreateCommentDto commentDto)
    {
        HttpResponseMessage httpResponse =
            await client.PostAsJsonAsync("comments", commentDto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<CommentDto>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }
}