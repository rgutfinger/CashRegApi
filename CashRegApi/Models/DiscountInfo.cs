using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashRegApi.Models
{
	public class DiscountInfo
	{
		public int TotalCount;
		public int FreeCount;

		public static DiscountInfo Create(DiscountInfoEx dex)
		{
			DiscountInfo info = new DiscountInfo();
			info.TotalCount = dex.TotalCount;
			info.FreeCount = dex.FreeCount;

			return info;
		}
	}
}