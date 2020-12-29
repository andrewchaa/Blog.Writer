using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Writer.Contracts.ApiModels;
using Blog.Writer.Contracts.Apis;
using Blog.Writer.Contracts.DomainModels;
using Blog.Writer.Contracts.Queries;
using MediatR;
using SanPellgrino;

namespace Blog.Writer.Services.Handlers
{
    public class PullPostsQueryHandler : IRequestHandler<PullPostsQuery, IEnumerable<BlogPost>>
    {
        private readonly IGithubApi _api;
        private PullPostsQuery _query;

        public PullPostsQueryHandler(IGithubApi api)
        {
            _api = api;
        }
        
        public async Task<IEnumerable<BlogPost>> Handle(PullPostsQuery query, 
            CancellationToken cancellationToken)
        {
            _query = query;

            var contentsResponse = await _api.GetRepositoryContentsList(_query.Owner,
                _query.Name, 
                "");
            var rootPosts = contentsResponse.Content
                .Where(x => x.Type == ItemType.File &&
                            !x.Name.Equals("Summary.md", StringComparison.OrdinalIgnoreCase))
                .ToList();
            
            var posts = new List<BlogPost>();
            posts.AddRange(await GetPosts(string.Empty, rootPosts));

            var groups = contentsResponse.Content
                .Where(x => x.Type == ItemType.Dir &&
                            !x.Name.Equals(".gitbook", StringComparison.OrdinalIgnoreCase))
                .ToList();
            foreach (var group in groups)
            {
                var groupResponse = await _api.GetRepositoryContentsList(_query.Owner,
                    _query.Name, 
                    group.Name);
                posts.AddRange(await GetPosts(group.Name, groupResponse.Content));
            }

            return posts;
        }

        private async Task<IEnumerable<BlogPost>> GetPosts(string path, 
            IEnumerable<PostResponse> postResponses)
        {
            var posts = new List<BlogPost>();
            
            foreach (var post in postResponses)
            {
                Console.WriteLine($"Pulling {post.Name} from {path} ...");
                var contentResponse = await _api.GetRepositoryContent(_query.Owner,
                    _query.Name,
                    post.Path);
                var commitsReponse = await _api.ListCommits(_query.Owner,
                    _query.Name,
                    post.Path);
                posts.Add(new BlogPost(post.Name,
                    commitsReponse.Content.First().Commit.Author.Date,
                    path,
                    contentResponse.Content.Content.FromBase64ToUtf8String()));
            }

            return posts;
        }
    }
}