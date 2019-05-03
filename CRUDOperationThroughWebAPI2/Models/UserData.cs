using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDOperationThroughWebAPI2.Models
{
	public class UserData
	{
		public int Uid { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
	}
}