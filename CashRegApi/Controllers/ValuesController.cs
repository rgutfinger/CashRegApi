using CashRegApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CashRegApi.Controllers
{
	// ASSUMPTIONS:
	// 1. Only one person using (=calling) the cash register


	[RoutePrefix("Api/Values")]
	public class ValuesController : ApiController
	{
		static List<ScanData> s_data = new List<ScanData>();
		static double? PercentDiscount = null;
		//static Dictionary<DiscountInfo> s_data = new List<ScanData>();

		// GET api/values
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		[HttpGet]
		[Route("TotalCost")]
		public List<ScanData> GetTotalCost()
		{
			return s_data;
		}
		// GET api/values/5

		public string Get(int id)
		{
			return "value";
		}

		// POST api/values

		[HttpPost]
		[Route("ScanByQuantity")]
		public HttpResponseMessage ScanByQuantity([FromBody]ScanData data)
		{
			s_data.Add(data);

			//** test exception..or catch throw responseExc- ck std***

			return Request.CreateResponse(HttpStatusCode.OK, "Successful scan by quantity");
		}

		[HttpPost]
		[Route("ScanByWeight")]
		public HttpResponseMessage ScanByWeight([FromBody]ScanData data)
		{
			s_data.Add(data);

			return Request.CreateResponse(HttpStatusCode.OK, "Successful scan by weight");
		}


		// PUT api/values/5
		public void PutDiscountByPercent([FromBody]double percent)
		{
		}

		public void PutDiscountByCount([FromBody]double percent)
		{
		}


		// DELETE api/values/5
		public void Delete(int id)
		{
		}

		// DELETE all
		public void Restart()
		{

		}
	}
}
