using Entities;
using Repository;

namespace CLI.UI.Users;

public class CreateUserView(IUserRepository userRepository)
{
    private IUserRepository _userRepository = userRepository;

    public async Task DisplayCreateNewUserMenuAsync()
    {
        string ?username = null;
        string ?password = null;
        
        Console.Write("-----------------------\n" +
                      "    Create New User    \n" +
                      "-----------------------\n" +
                      "Username: ");
        
        username = Console.ReadLine();
        while (username == null || username.IsWhiteSpace())
        {
            Console.WriteLine("Username cannot be empty. Provide correct username:");
            username = Console.ReadLine();
        }
        
        Console.Write("Password: ");
        password = Console.ReadLine();
        while (password == null || password.IsWhiteSpace())
        {
            Console.WriteLine("Password cannot be empty. Provide correct password:");
            password = Console.ReadLine();
        }

        await CreateNewUserAsync(username, password);
        
        Console.WriteLine("User created successfully! Press enter to return back to main menu.\n");
        Console.ReadLine();
    }

    private async Task CreateNewUserAsync(string username, string password)
    {
        User newUser = new User(null, username, password);

        await _userRepository.AddAsync(newUser);
    }
}