using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcglobal.Models
{
	public class PurchaseDetail
	{
		//public decimal ChalanNo { get; set; }
		public decimal ItemId { set; get; }
		public decimal PurchasePrice { set; get; }
		public decimal SalesPrice { set; get; } 
		public decimal Quantity { set; get; }
	}
}