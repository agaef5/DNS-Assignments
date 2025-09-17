// See https://aka.ms/new-console-template for more information

using CLI.UI;
using InMemoryRepositories;
using Repository;

Console.WriteLine("Starting CLI app...");
IUserRepository userRepository = new InUserMemoryRepository();
ICommentRepository commentRepository = new InCommentMemoryRepository();
IPostRepository postRepository = new InPostMemoryRepository();

CliApp cliApp = new CliApp(postRepository, commentRepository, userRepository);
await cliApp.StartAsync();