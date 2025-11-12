using DTOs;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetPostById([FromBody] int id)
    {
        try
        {
            Post post = await postRepository.GetSingleAsync(id);
            var user = await userRepository.GetSingleAsync(post.UserId);
            List<Comment> comments = await commentRepository.GetMany();
            List<CommentDto> commentDtos = new List<CommentDto>();
            foreach (Comment comment in comments)
            {
                CommentDto dto = new CommentDto(comment.Id, comment.Body,
                    comment.PostId, comment.UserId);
                commentDtos.Add(dto);
            }

            PostDto postDto = new PostDto(post.Id, post.Title, post.Body,
                post.UserId, user.Username, commentDtos);
            return postDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}