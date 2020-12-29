using System;
using MediatR;

namespace Blog.Writer.Contracts.Commands
{
    public class PullImagesCommand : IRequest
    {
        public string Owner { get; }
        public string Name { get; }
        public string Directory { get; }

        public PullImagesCommand(string owner, 
            string name,
            string directory)
        {
            Owner = owner;
            Name = name;
            Directory = directory;
        }
    }
}