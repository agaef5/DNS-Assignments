using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace WebAPI.Controllers;

[ApiController]
[Route("controller")]
public class CommentsController(ICommentRepository commentRepository)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CommentDto>> 
        CreateComment([FromBody] CreateCommentDto request)
    {
        try
        { 
            Comment created = await commentRepository.AddAsync(request.Body, request.PostId, request.UserId);
            CommentDto dto = new CommentDto(created.Id, created.Body,
                created.PostId, created.UserId);

            return Redirect($"posts/{dto.PostId}");
        }    
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
}