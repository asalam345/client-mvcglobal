using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcglobal.Models
{
	public class Purchase
	{
		public decimal ChalanNo { get; set; }
		public decimal SupplierId { get; set; }
		public decimal CreatedBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string IPAddress { get; set; }
	}
}