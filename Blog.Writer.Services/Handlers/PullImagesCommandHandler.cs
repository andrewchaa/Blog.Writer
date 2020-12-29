using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Blog.Writer.Contracts.Apis;
using Blog.Writer.Contracts.Commands;
using MediatR;

namespace Blog.Writer.Services.Handlers
{
    public class PullImagesCommandHandler : IRequestHandler<PullImagesCommand>
    {
        private readonly IGithubApi _api;

        public PullImagesCommandHandler(IGithubApi api)
        {
            _api = api;
        }
        
        public async Task<Unit> Handle(PullImagesCommand command, 
            CancellationToken cancellationToken)
        {
            var listResponse = await _api.GetRepositoryAssets(command.Owner,
                command.Name);

            foreach (var asset in listResponse.Content)
            {
                Console.WriteLine($"Downloading assets, {asset.Name} ...");
                var contentResponse = await _api.GetBlob(command.Owner,
                    command.Name,
                    asset.Sha);
                
                await File.WriteAllBytesAsync(Path.Combine(command.Directory, asset.Name), 
                    Convert.FromBase64String(contentResponse.Content.Content));                
            }

            return new Unit();
        }

    }
}