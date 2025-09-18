using Entities;
using Repository;

namespace CLI.UI.Posts;

public class PostOverview(IPostRepository postRepository,IUserRepository userRepository, ICommentRepository commentRepository)
{
    private IPostRepository _postRepository = postRepository;
    private IUserRepository _userRepository = userRepository;
    private ICommentRepository _commentRepository = commentRepository;

    public async Task DisplayPostOverviewAsync()
    {
        Console.Write("-----------------------\n" +
                      "     Posts Overview    \n" +
                      "-----------------------\n" );
        
        List<Post> posts = _postRepository.GetMany().ToList();

        if (posts.Any())
        {
            foreach (var post in posts)
            {
                await DisplayPostAsync(post);
            }
        }
        
        Console.Write("-----------------------\n" +
                      "If you want to see specific post and its comment, write post id. " +
                      "Else press enter to return to main menu" +
                      "Provide post id: " );
        
        string? postIdInput = Console.ReadLine();
        if (postIdInput != null)
        {
            int postId = Convert.ToInt32(postIdInput);
            await DisplayPostDetailsAsync(postId);
        } 
    }

    private async Task DisplayPostAsync(Post post)
    {
        Console.Write("postID: " + post.id + ", userID: " + post.userId +
                      "\n-----------------------\n" +
                      post.title +
                      "\n-----------------------\n" +
                       "\n \n");
        
    }

    private async Task DisplayPostDetailsAsync(int postId)
    {
        SinglePostView singlePostView = new SinglePostView(postRepository, userRepository, commentRepository);
        await singlePostView.DisplaySinglePostAsync(postId);
    }
}