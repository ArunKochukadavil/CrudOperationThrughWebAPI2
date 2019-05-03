using CRUDOperationThroughWebAPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRUDOperationThroughWebAPI2.Controllers
{
	public class ValuesController : ApiController
	{
		[HttpGet]
		[Route("api/values")]
		public IEnumerable<UserData> Get()
		{
			IEnumerable<UserData> data = new DataHelper().GetValuesFromTable();
			return data;
		}

		// GET api/<controller>/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<controller>
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
		}
	}
}