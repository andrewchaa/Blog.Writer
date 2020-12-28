using MediatR;

namespace Blog.Writer.Contracts.Commands
{
    public class WritePostsCommand : IRequest
    {
        public string Owner { get; }
        public string Name { get; }
        public string Directory { get; }

        public WritePostsCommand(string owner, 
            string name, 
            string directory)
        {
            Owner = owner;
            Name = name;
            Directory = directory;
        }
    }
}