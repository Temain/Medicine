using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PriceIssuer.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		// GET api/values
		[HttpGet]
		public async Task<ActionResult<IEnumerable<string>>> Get(CancellationToken cancellationToken)
		{
			var client = new HttpClient();
			var response = await client.GetAsync("https://auth.temain.tk/.well-known/openid-configuration");

			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
