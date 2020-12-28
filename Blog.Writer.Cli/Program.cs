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
        private const string Write = "write";
        private static readonly IServiceProvider Services = Startup.Build();
        private static readonly IMediator _mediator = Services.GetService<IMediator>();
        private static readonly RepositoryOptions RepositoryOptions = Services.GetService<RepositoryOptions>();

        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<Options>(args)
                .WithParsedAsync(async x =>
                {
                    if (string.IsNullOrEmpty(x.Command))
                    {
                        Console.WriteLine("Usage: \n");
                        Console.WriteLine("dotnet run");
                        Console.WriteLine($"  --cmd {Write}     to generate jekyll posts from your chosen github repository");
                        return;
                    }

                    if (x.Command == Write)
                    {
                        Console.WriteLine("Pulling github posts ...");
                        await _mediator.Send(new WritePostsCommand(RepositoryOptions.Owner,
                            RepositoryOptions.Name));
                    }
                });
        }
    }
}