using DTOs;
using DTOs.Comments;
using DTOs.Posts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace WebAPI.Controllers;

[ApiController]
[Route("controller")]
public class PostsController(IPostRepository postRepository, ICommentRepository commentRepository, IUserRepository userRepository) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePost(
        [FromBody] CreatePostDto request)
    {
        try
        {
            Post created = await postRepository.AddAsync(request.Title, request.Body, request.UserId);
            var user = await userRepository.GetSingleAsync(created.UserId);
            PostDto dto = new PostDto(created.Id, created.Title, created.Body,
                created.UserId, user.Username, null);
            return Created($"posts/{dto.Id}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<PostDto>>> GetAllPosts()
    {
        try
        {
            List<Post> posts = await postRepository.GetMany();
            List<Comment> comments = await commentRepository.GetMany();
            
            List<PostDto> postDtos = new List<PostDto>();
            foreach (var post in posts)
            {
                postDtos.Add(await GetPostDtoWithComments(post, comments));
            }

            return postDtos;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetPostById([FromBody] int id)
    {
        try
        {
            Post post = await postRepository.GetSingleAsync(id);
            var comments = await commentRepository.GetMany();
            
            return await GetPostDtoWithComments(post, comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    private async Task<PostDto> GetPostDtoWithComments(Post post, List<Comment> comments)
    {
        User postUser = await userRepository.GetSingleAsync(post.UserId);
        PostDto dto = Post.ToDto(post, postUser.Username,
            new List<CommentDto>());
        
        foreach (Comment comment in comments)
        {
            if (comment.PostId == dto.Id)
            {
                User commentUser = await 
                    userRepository.GetSingleAsync(comment.UserId);
                CommentDto commentDto =
                    Comment.ToDto(comment, commentUser.Username);
                dto.Comments.Add(commentDto);
            }
        }
        return dto;
    }
}