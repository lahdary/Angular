using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Angular.Dtos;
using Angular.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
    public class BitcoinController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/test
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] TickerBodyDto body)
        {
            var url = "https://min-api.cryptocompare.com/data/price?fsym=ETH&tsyms=USD";

            HttpResponseMessage httpResponse  = await HttpRequestFactory.Post(url, body);
            var stringData = httpResponse.ContentAsString();


            return Ok( new{     
                reason = "ok",
                content = stringData
            });

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }