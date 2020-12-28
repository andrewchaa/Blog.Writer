using System;
using System.IO;
using System.Net.Http;
using Blog.Writer.Contracts.Apis;
using Blog.Writer.Contracts.Configurations;
using Blog.Writer.Services.Handlers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace Blog.Writer.Cli.Infrastructure
{
    public class Startup
    {
        private static readonly IServiceCollection Services = new ServiceCollection();

        public static IServiceCollection ConfigureServices()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddEnvironmentVariables();
            configBuilder.AddJsonFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "github.json"), optional: false, reloadOnChange: true);
            configBuilder.AddJsonFile($"appsettings.json", optional: false);
#if DEBUG
            configBuilder.AddJsonFile($"appsettings.Development.json", true);
#endif
            var config = configBuilder.Build();
            Services.Configure<CredentialsOptions>(config);
            Services.Configure<RepositoryOptions>(config.GetSection("Repository"));

            Services.AddSingleton(x => x.GetService<IOptions<CredentialsOptions>>().Value)
                .AddSingleton(x => x.GetService<IOptions<RepositoryOptions>>().Value)
                .AddSingleton(x =>
                {
                    var credentials = x.GetService<CredentialsOptions>();
                    var handler = new AuthHandler(credentials.Token);
                    var httpClient = new HttpClient(handler)
                    {
                        BaseAddress = new Uri("https://api.github.com"),
                    };
                    return RestService.For<IGithubApi>(httpClient);
                })
                ;

            Services.AddMediatR(typeof(PullPostsQueryHandler).Assembly);
            
            return Services;
        }

        public static IServiceProvider Build()
        {
            return ConfigureServices()
                .BuildServiceProvider();
        } 
    }
}