using DTOs.Posts;

namespace ClientApp.Services.Post;

public interface IPostService
{
     Task<PostDto> CreatePostAsync(CreatePostDto postDto);
     Task<List<PostDto>> GetAllPostsAsync();
     Task<PostDto> GetPostByIdAsync(int id);
}