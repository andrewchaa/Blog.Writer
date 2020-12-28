using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Writer.Contracts.ApiModels;
using Blog.Writer.Contracts.Apis;
using Blog.Writer.Contracts.Commands;
using Blog.Writer.Contracts.DomainModels;
using Blog.Writer.Contracts.Queries;
using MediatR;
using SanPellgrino;

namespace Blog.Writer.Services.Handlers
{
    public class PullPostsQueryHandler : IRequestHandler<PullPostsQuery, IEnumerable<GithubPost>>
    {
        private readonly IGithubApi _api;

        public PullPostsQueryHandler(IGithubApi api)
        {
            _api = api;
        }
        
        public async Task<IEnumerable<GithubPost>> Handle(PullPostsQuery query, 
            CancellationToken cancellationToken)
        {
            var posts = new List<GithubPost>();

            var contentsResponse = await _api.GetRepositoryContents(query.Owner, 
                query.Name, "");
            foreach (var post in contentsResponse.Content
                .Where(x => !x.Name.Equals("Summary.md", StringComparison.OrdinalIgnoreCase)))
            {
                if (post.Type == ItemType.Dir)
                {
                    continue;
                }
                
                Console.WriteLine($"Pulling {post.Name} ...");
                var contentResponse = await _api.GetRepositoryContent(query.Owner,
                    query.Name,
                    post.Path);
                var commitsReponse = await _api.ListCommits(query.Owner,
                    query.Name,
                    post.Path);
                posts.Add(new GithubPost(post.Name,
                    commitsReponse.Content.First().Commit.Author.Date,
                    contentResponse.Content.Content.FromBase64ToUtf8String()));
                
            }

            return posts;
        }
    }
}