using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Extensions;
using System.Net.Http;
using System.Threading.Tasks;

namespace RickAndMorty.Controllers
{
    public class GithubControlller : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        public GithubControlller(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("api/github/repos")]
        public async Task<IActionResult> GetMyRepos()
        {
            var client = _httpClientFactory.CreateClient();

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, 
                "https://api.github.com/users/xhantimda/repos");

            requestMessage.Headers.Add("Accept", "application/vnd.github.v3+json");
            requestMessage.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var response = await client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    return Ok(stream.Deserialize<object>());
                }
            }

            return NotFound();
        } 
    }
}
