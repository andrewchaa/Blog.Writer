using System.Collections.Generic;
using Blog.Writer.Contracts.DomainModels;
using MediatR;

namespace Blog.Writer.Contracts.Queries
{
    public class PullPostsQuery : IRequest<IEnumerable<BlogPost>>
    {
        public string Owner { get; }
        public string Name { get; }

        public PullPostsQuery(string owner, 
            string name)
        {
            Owner = owner;
            Name = name;
        }
    }
}