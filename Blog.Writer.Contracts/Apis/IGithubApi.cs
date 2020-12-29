
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Writer.Contracts.ApiModels;
using Refit;

namespace Blog.Writer.Contracts.Apis
{
    public interface IGithubApi
    {
        [Get("/repos/{owner}/{repo}/contents/{path}")]
        Task<IApiResponse<IEnumerable<PostResponse>>> GetRepositoryContentsList(string owner,
            string repo,
            string path);

        [Get("/repos/{owner}/{repo}/contents/{path}")]
        Task<IApiResponse<PostResponse>> GetRepositoryContent(string owner,
            string repo,
            string path);

        [Get("/repos/{owner}/{repo}/contents/.gitbook/assets")]
        Task<IApiResponse<IEnumerable<PostResponse>>> GetRepositoryAssets(string owner,
            string repo);

        [Get("/repos/{owner}/{repo}/git/blobs/{sha}")]
        Task<IApiResponse<BlobResponse>> GetBlob(string owner,
            string repo, 
            string sha);

        [Get("/repos/{owner}/{repo}/commits?path={path}")]
        Task<IApiResponse<IEnumerable<CommitResponse>>> ListCommits(string owner,
            string repo,
            string path);
    }
}