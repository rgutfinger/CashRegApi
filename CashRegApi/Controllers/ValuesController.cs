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
		static double? s_percentDiscount = null;
		static Dictionary<string, DiscountInfo> s_discounts = new Dictionary<string, DiscountInfo>();

		// GET api/values
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		[HttpGet]
		[Route("TotalCost")]
		public TotalData GetTotalCost()
		{
			TotalData td = new TotalData();
			td.Data = s_data;

			double total = 0;//compute price.. ***
			for (int ix=0; ix<s_data.Count(); ix++)
			{
				ScanData data = s_data[ix];
				total += CalcPrice(data);
				// next discount
			}
			
			if (s_percentDiscount.HasValue)
			{
				double discount = s_percentDiscount.Value / 100.0;
				total = total * (1 - discount);
			}

			td.Total = total;

			return td;
		}

		private double CalcPrice(ScanData data)
		{
			double price=0.0;
			double itemPrice = FetchPrice(data.Code);
			if (data.Quantity > 0)
				price = itemPrice * data.Quantity;
			else if (data.Weight > 0)
				price = itemPrice * data.Weight;

			return price;
		}

		private double FetchPrice(string code)
		{
			// In the official product, this method would read the price for the given code (say from a database)
			// For now, it just 'translates' the code to a double as the price

			double price=0.0;
			if (!double.TryParse(code, out price))
				throw new Exception("No price for code"); //refine err

			return price;
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
		[HttpPut]
		[Route("DiscountByPercent/{percent:double}")]
		public HttpResponseMessage DiscountByPercent([FromUri] double percent)
		{
			s_percentDiscount = percent;

			return Request.CreateResponse(HttpStatusCode.OK, "Successful % discount");
		}

		[HttpPut]
		[Route("DiscountByCount")]
		public HttpResponseMessage DiscountByCount([FromBody]DiscountInfoEx discInfoEx)
		{
			DiscountInfo di = DiscountInfo.Create(discInfoEx);
			if (!s_discounts.ContainsKey(discInfoEx.Code))
				s_discounts.Add(discInfoEx.Code, di);
			else
				throw new Exception("Repeated discount"); //test refine err

			return Request.CreateResponse(HttpStatusCode.OK, "Successful discount");
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
