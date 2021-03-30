using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Function
{
    public class FunctionHandler
    {
        public async Task<Response> Handle(HttpRequest request)
        {
            var reader = new StreamReader(request.Body);
            var input = await reader.ReadToEndAsync();

            return new Text(System.Net.HttpStatusCode.OK, $"Hello! Your input was {input}", "text/plain");
        }
    }
}