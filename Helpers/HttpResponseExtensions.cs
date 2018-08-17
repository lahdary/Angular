using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Angular.Helpers
{
    /// <summary>
    /// Class responsible for converting HttpresponseMessage to JSON, string and handling responses  
    /// </summary>
    public static class HttpResponseExtensions
    {
        public static T ContentAsType<T>(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;

            return string.IsNullOrEmpty(data) ? 
                            default(T) : 
                            JsonConvert.DeserializeObject<T>(data);
        }

        public static string ContentAsJson(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            
            return JsonConvert.SerializeObject(data);
        }

        public  static ContentResult Handle(this HttpResponseMessage response){
            return null;
        }


        public static string ContentAsString(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
