using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Alpha.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ApiProxyController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Route("{**path}")]
        [HttpGet]
        public async Task<IActionResult> ProxyGet(string path)
        {
            return await PerformProxy(HttpMethod.Get, path, "" );
        }

        [Route("{**path}")]
        [HttpPost]
        public async Task<IActionResult> ProxyPost(string path, [FromBody] object requestBody)
        {
            return await PerformProxy(HttpMethod.Post, path, requestBody?.ToString() ?? "" );
        }

        private async Task<IActionResult> PerformProxy(HttpMethod method, string path, string requestBody )
        {
            var target = "http://localhost:8080/api/" + path;
            HttpRequestMessage requestMessage;

            if( method == HttpMethod.Get )
            {
                requestMessage = new HttpRequestMessage(HttpMethod.Get, target);
            }
            else
            {
                var contentType = (Request.ContentType ?? "").Split(";")[0];
                requestMessage =  new HttpRequestMessage(method, target)
                    {
                        Content = new StringContent(requestBody.ToString() ?? "", 
                            new MediaTypeHeaderValue(contentType ?? "application/json"))
                    };
            }

            requestMessage.Headers.TryAddWithoutValidation("Authorization", Request.Headers.Authorization.ToArray());

            var response = await _httpClient.SendAsync(requestMessage);

            var result = new ContentResult
            {
                Content = await response.Content.ReadAsStringAsync(),
                StatusCode = (int)response.StatusCode
            };

            Response.ContentType = response.Content.Headers.ContentType?.ToString();

            return result;
        }
    }
}