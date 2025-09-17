using Entities;
using Repository;

namespace CLI.UI.Posts;

public class PostOverview(IPostRepository postRepository)
{
    private IPostRepository _postRepository = postRepository;

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
        Console.Write("-----------------------\n" +
                      "postID: " + post.id + "userID: " + post.userId +
                      "\n-----------------------\n" +
                      post.title +
                      "\n-----------------------\n" +
                      post.body + "\n");
        
    }

    private async Task DisplayPostDetailsAsync(int postId)
    {
        
    }
}