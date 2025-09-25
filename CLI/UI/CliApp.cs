using CLI.UI.Posts;
using CLI.UI.Users;
using Entities;
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
        await CreateDummyData();
        
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
                    CreatePostView postCreateView = new CreatePostView(postRepository, userRepository);
                    await postCreateView.DisplayCreateNewPostMenu();
                    break;
                }
                case 3:
                {
                    PostOverview postOverview = new PostOverview(postRepository, userRepository, commentRepository);
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
        List<User> users = [
            new(null, "mariposa", "password"),
            new(null, "burrito", "potato"),
            new(null, "capsle", "soda")
        ];

        foreach (var user in users) await userRepository.AddAsync(user);
        
        //
        // List<Post> posts =
        // [
        //     new(null, "First flight", "Just joined here, excited to share my thoughts!", 1),
        //     new(null, "Potato time", "Burrito filled with potatoes is the best invention.", 2),
        //     new(null, "Capsule vibes", "Soda cans clinking while coding all night.", 3),
        //     new(null, "Nature walk", "Mariposa here, I love walking in the forest after rain.", 1),
        //     new(null, "Random thought", "Why does every good idea come at 2 AM?", 2)
        // ];
        //
        // foreach (var post in posts)
        //     await postRepository.AddAsync(post);
        //
        // List<Comment> comments = [
        //     new(null, "Welcome aboard, looking forward to your posts!", 1, 2), // burrito on mariposa's post
        //     new(null, "Totally agree, potatoes make everything better.", 2, 1), // mariposa on burrito's post
        //     new(null, "Haha, soda coding energy is real!", 3, 2), // burrito on capsle's post
        //     new(null, "That sounds peaceful. I need a forest walk too.", 4, 3), // capsle on mariposa's post
        //     new(null, "Relatable! My brain only works at night.", 5, 3) // capsle on burrito's post
        // ];
        //
        // foreach (var comment in comments)
        //     await commentRepository.AddAsync(comment);
        //
    }
}