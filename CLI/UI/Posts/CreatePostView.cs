using Entities;
using Repository;

namespace CLI.UI.Posts;

public class CreatePostView(IPostRepository postRepository, IUserRepository userRepository)
{
    private IPostRepository _postRepository = postRepository;
    private IUserRepository _userRepository = userRepository;

    public async Task DisplayCreateNewPostMenu()
    {
        int userId = -1;
        string? title = null;
        string? body = null;

        Console.Write("-----------------------\n" +
                      "    Create New Post    \n" +
                      "-----------------------\n" +
                      "UserId: ");

        while (true)
        {
            string input = Console.ReadLine();
            
            if (!int.TryParse(input, out userId) || userId < 1)
            {
                Console.WriteLine("UserId is incorrect. Try again.");
                continue;
            }

            User? user = await CheckIfUserExistsAsync(userId);
            
            if (user == null)
            {
                Console.WriteLine("No user with such id exists. Press enter to return.");
                Console.ReadLine();
                return;
            }
            break;
        }

        Console.Write("Post title: ");
        title = Console.ReadLine();
        while (title == null || title.IsWhiteSpace())
        {
            Console.WriteLine("Title cannot be empty. Try again:");
            title = Console.ReadLine();
        }

        Console.Write("Post body: ");
        body = Console.ReadLine();
        while (body == null || body.IsWhiteSpace())
        {
            Console.WriteLine("Body cannot be empty. Try again:");
            body = Console.ReadLine();
        }

        await CreateNewPostAsync(userId, title, body);
        Console.WriteLine("Post created successfully! Press enter to return back to main menu.\n");
        Console.ReadLine();
    }

    private async Task<User?> CheckIfUserExistsAsync(int userId)
    {
        var returnedUser = await _userRepository.GetSingleAsync(userId);
        return returnedUser;
    }

    private async Task CreateNewPostAsync(int userId, string title, string body)
    {
        Post newPost = new Post(null, title, body, userId);

        await _postRepository.AddAsync(newPost);
    }

}