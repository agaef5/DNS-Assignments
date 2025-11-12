using DTOs;
using DTOs.Users;

namespace ClientApp.Services.User;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(AuthUserDto request); 
}