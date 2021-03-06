using MediatR;

namespace Blog.Writer.Contracts.Commands
{
    public class WritePostsCommand : IRequest
    {
        public string Owner { get; }
        public string Name { get; }
        public string Directory { get; }
        public string ImageDirectory { get; }

        public WritePostsCommand(string owner, 
            string name, 
            string directory, 
            string imageDirectory)
        {
            Owner = owner;
            Name = name;
            Directory = directory;
            ImageDirectory = imageDirectory;
        }
    }
}