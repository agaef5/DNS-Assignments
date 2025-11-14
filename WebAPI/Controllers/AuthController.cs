using DTOs.Users;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IUserRepository userRepository) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> RegisterNewUser([FromBody] RegisterRequest request)
    {
        try
        {
            User? existingUser =
                await userRepository.GetSingleAsync(request.Username);
            if (existingUser != null)
            {
                return StatusCode(401, "This username is already in use");
            }

            var newUser = await
                userRepository.AddAsync(request.Username, request.Password);
            
            return new UserDto(newUser.Id, newUser.Username);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(401, "Unable to register. Try again later.");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginUser([FromBody] RegisterRequest request)
    {
        try
        {
            User? user =
                await userRepository.GetSingleAsync(request.Username);
            if (user is null)
            {
                return StatusCode(401, "No user with such username exists");
            }

            if (!user.Password.Equals(request.Password))
            {
                return StatusCode(401, "Password is incorrect");
            }
            
            return new UserDto(user.Id, user.Username);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(401, "Unable to log in. Try again later.");
        }
    }
    
}