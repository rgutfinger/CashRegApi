using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CashRegApi.Models
{
	public class ScanData
	{
		[Required]
		public string Code;

		public int Quantity;
		public double Weight;
	}
}