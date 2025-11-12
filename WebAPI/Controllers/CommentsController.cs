using DTOs;
using DTOs.Comments;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace WebAPI.Controllers;

[ApiController]
[Route("controller")]
public class CommentsController(ICommentRepository commentRepository, IUserRepository userRepository)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CommentDto>> 
        CreateComment([FromBody] CreateCommentDto request)
    {
        try
        { 
            Comment created = await commentRepository.AddAsync(request.Body, request.PostId, request.UserId);
            User user = await userRepository.GetSingleAsync(created.UserId);
            CommentDto dto = new CommentDto(created.Id, created.Body,
                created.PostId, created.UserId, user.Username);

            return Redirect($"posts/{dto.PostId}");
        }    
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
}