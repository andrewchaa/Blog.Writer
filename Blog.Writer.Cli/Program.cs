using System;
using System.Threading.Tasks;
using Blog.Writer.Cli.Infrastructure;
using Blog.Writer.Contracts.Commands;
using Blog.Writer.Contracts.Configurations;
using CommandLine;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Writer.Cli
{
    class Program
    {
        private const string Pull = "pull";
        private static readonly IServiceProvider Services = Startup.Build();
        private static readonly IMediator _mediator = Services.GetService<IMediator>();
        private static readonly RepositoryOptions RepositoryOptions = Services.GetService<RepositoryOptions>();

        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<Options>(args)
                .WithParsedAsync(async x =>
                {
                    x.Command = Pull;
                    
                    if (string.IsNullOrEmpty(x.Command))
                    {
                        Console.WriteLine("Usage: \n");
                        Console.WriteLine("dotnet run");
                        Console.WriteLine($"  --cmd {Pull}     to pull all your posts from the github repository");
                        return;
                    }

                    if (x.Command == Pull)
                    {
                        Console.WriteLine("Pulling github posts ...");
                        await _mediator.Send(new WritePostsCommand(RepositoryOptions.Owner,
                            RepositoryOptions.Name));
                    }
                });
        }
    }
}