using Entities;
using Repository;

namespace CLI.UI.Posts;

public class SinglePostView (IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
{
    private IPostRepository _postRepository = postRepository;
    private IUserRepository _userRepository = userRepository;
    private ICommentRepository _commentRepository = commentRepository;

    public async Task DisplaySinglePostAsync(int postId)
    {
        while (true)
        {
            await GetAndShowPostDetailsAsync(postId);

            Console.WriteLine("c - comment      b - back to main menu");
            string input = Console.ReadLine();
            
            if(input == "b") return;
            if (input == "c")
            {
                await WriteComment(postId);
            }
        }
    }

    private async Task GetAndShowPostDetailsAsync(int postId)
    {
        Post post = await _postRepository.GetSingleAsync(postId);
        
        Console.Write("postID: " + post.id + ", userID: " + post.userId +
                      "\n-----------------------\n" +
                      post.title +
                      "\n-----------------------\n" +
                      post.body + "\n \n");

        await DisplayCommentsAsync(postId);
    }

    private async Task DisplayCommentsAsync(int postId)
    {
        List<Comment> comments =  _commentRepository.GetMany().Where(c => c.postId == postId).ToList();
        if (comments.Any())
        {
            foreach (var comment in comments)
            {
                Console.Write("ID: " + comment.id + ", userID: " + comment.userId +
                              "\n-----------------------\n" +
                              comment.body +
                              "\n_______________________\n");
            }
        }
        else
        {
            Console.Write("\n-----------------------\n" +
                          "No comments" + 
                          "\n_______________________\n");
        }
    }

    private async Task WriteComment(int postId)
    {
        Console.Write("\n-----------------------\n" +
                      "        Write comment      " +
                      "\n_______________________\n" +
                      "User Id:");

        int userId;
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
        
        Console.WriteLine("Write your comment:\n");
        string body = Console.ReadLine();
        while (body == null || body.IsWhiteSpace())
        {
            Console.WriteLine("Comment cannot be empty:\n");
            body = Console.ReadLine();
        }

        await CreateCommentAsync(postId, userId, body);
    }
    
    private async Task<User?> CheckIfUserExistsAsync(int userId)
    {
        var returnedUser = await _userRepository.GetSingleAsync(userId);
        return returnedUser;
    }

    private async Task CreateCommentAsync(int postId, int userId, string body)
    {
        Comment newComment = new Comment(null, body, postId, userId);
        await _commentRepository.AddAsync(newComment);
    }
}