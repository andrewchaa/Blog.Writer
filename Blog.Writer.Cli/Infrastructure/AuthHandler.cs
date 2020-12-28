using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Writer.Cli.Infrastructure
{
    public class AuthHandler : HttpClientHandler
    {
        private readonly string _token;

        public AuthHandler(string token)
        {
            _token = token;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
            CancellationToken cancellationToken)
        {
            request.Headers.Add("User-Agent", "Blow.Writer");
            request.Headers.Add("Authorization", $"Bearer {_token}");

            var response = await base.SendAsync(request, cancellationToken)
                .ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
                Console.WriteLine(await response.Content.ReadAsStringAsync());

            return response;
        }
    }
}