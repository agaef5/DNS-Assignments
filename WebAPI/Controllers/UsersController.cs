using DTOs;
using DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace WebAPI.Controllers;

[ApiController]
[Route("controller")]
public class UsersController(IUserRepository userRepository) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUserDto(
        [FromBody] AuthUserDto request)
    {
        try
        {
            Entities.User created =
                await userRepository.AddAsync(request.Username,
                    request.Password);
            UserDto dto = new UserDto(created.Id, created.Username);
            return dto;
        }       
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}