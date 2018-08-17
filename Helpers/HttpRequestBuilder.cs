using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Angular.Helpers
{
    public class HttpRequestBuilder
    {
        private HttpMethod method = null;
        private string requestUri = "";
        private HttpContent content = null;
        private string basicToken = "";
        private string acceptHeader = "application/json";
        private TimeSpan timeout = new TimeSpan(0, 5, 0);

        public HttpRequestBuilder()
        {
        }

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }
        
        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            return this;
        }

        public HttpRequestBuilder AddBasicToken(string basicToken)
        {
            this.basicToken = basicToken;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }
        
        public async Task<HttpResponseMessage> SendAsync()
        {

            // Setup request
            var request = new HttpRequestMessage
            {
                Method = this.method,
                RequestUri = new Uri(this.requestUri)
            };

            if (this.content != null)
                request.Content = this.content;

        
            request.Headers.Add("X-API-KEY","abc7532b77826fd628bda18ffca5e0be");
            request.Headers.Add("X-API-SECRET","f4dd0ad447cec12e45710b3fe098995a");

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(this.acceptHeader))
                request.Headers.Accept.Add(
                   new MediaTypeWithQualityHeaderValue(this.acceptHeader));
            
            // Setup client
            var client = new System.Net.Http.HttpClient();
            client.Timeout = this.timeout;
            
            return await client.SendAsync(request);
        }
    }   
}