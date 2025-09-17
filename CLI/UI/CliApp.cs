using CLI.UI.Posts;
using CLI.UI.Users;
using Repository;

namespace CLI.UI;

public class CliApp
{
    private IPostRepository postRepository;
    private ICommentRepository commentRepository;
    private IUserRepository userRepository;

    public CliApp(IPostRepository postRepository, ICommentRepository commentRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
    }

    public async Task StartAsync()
    {
        while (true)
        {
            int optionChosen = await DisplayMainMenuAsync();
            Console.WriteLine("This is the chosen option: " + optionChosen);
            
            switch (optionChosen)
            {
                case 1:
                {
                    CreateUserView userView = new CreateUserView(userRepository);
                    await userView.DisplayCreateNewUserMenuAsync();
                    break;
                }
                case 2:
                {
                    break;
                }
                case 3:
                {
                    PostOverview postOverview = new PostOverview(postRepository);
                    await postOverview.DisplayPostOverviewAsync();
                    break;
                } 
            }
        }
    }

    
    public async Task<int> DisplayMainMenuAsync()
    {
        Console.Write("Write a number out of the list and press enter:\n" +
                      "1. Create new user\n" +
                      "2. Create new post\n" +
                      "3. View posts\n" +
                      "\n");

        int option = 0;
        while (option is < 1 or > 3)
        {
            Console.Write("\nWrite number: ");
            option = Convert.ToInt16(Console.ReadLine());
        }

        return option;
    }

    public async Task CreateDummyData()
    {
        // TODO
    }
}