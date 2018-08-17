using System.Net.Http;
using System.Threading.Tasks;

namespace Angular.Helpers
{
    public class HttpRequestFactory
    {
        public static async Task<HttpResponseMessage> Get(string requestUri)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Post(
           string requestUri, object value)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value)) ;

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Put(
           string requestUri, object value)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value));

            return await builder.SendAsync();
        }

        
    }
}