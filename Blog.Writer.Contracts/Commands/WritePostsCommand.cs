using MediatR;

namespace Blog.Writer.Contracts.Commands
{
    public class WritePostsCommand : IRequest
    {
        public string Owner { get; }
        public string Name { get; }

        public WritePostsCommand(string owner, 
            string name)
        {
            Owner = owner;
            Name = name;
        }
    }
}