using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CashRegApi.Controllers
{
	[RoutePrefix("RegApi")]
	public class ValuesController : ApiController
	{
		// GET api/values
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values

		[HttpPost]
		[Route("ScanByQuanity")]
		public void Post([FromBody]string code, [FromBody] int quantity)
		{
		}

		[HttpPost]
		[Route("ScanByWeight")]
		public void Post([FromBody]string code, [FromBody] double weight)
		{
		}


		// PUT api/values/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}
