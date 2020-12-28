using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Writer.Contracts.Commands;
using Blog.Writer.Contracts.Queries;
using MediatR;

namespace Blog.Writer.Services.Handlers
{
    public class WritePostsCommandHandler : IRequestHandler<WritePostsCommand>
    {
        private readonly IMediator _mediator;

        public WritePostsCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<Unit> Handle(WritePostsCommand command, 
            CancellationToken cancellationToken)
        {
            var posts = await _mediator.Send(new PullPostsQuery(command.Owner,
                command.Name));

            if (Directory.Exists("posts"))
            {
                Directory.Delete("posts", true);
            }
            Directory.CreateDirectory("posts");
            
            foreach (var post in posts)
            {
                var title = post.Contents.Split(Environment.NewLine)
                    .First()
                    .TrimStart('#',' ')
                    .TrimStart('.');
                Console.WriteLine(title);
                
                await File.WriteAllTextAsync(post.Path, 
                    $"{post.FrontMatter}\n{post.Contents}");
            }
            
            return new Unit();
        }
    }
}