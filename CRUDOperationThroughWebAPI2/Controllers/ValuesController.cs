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
		/// <summary>
		/// for fetching the latest records from database
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("api/values/retreive")]
		public List<UserData> Get()
		{
			return new DataHelper().GetValuesFromTable();
		}

		/// <summary>
		/// for updating the table with data send by client
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("api/values/update")]
		public string Put(UserData data)
		{
			var dataHelper = new DataHelper();
			try
			{
				var isUpdateSuccess = dataHelper.UpdateUserTable(data);
				return isUpdateSuccess.ToString();
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}


		/// <summary>
		/// for inserting new records
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("api/values/insert")]
		public string Post([FromBody]UserData data)
		{
			var dataHelper = new DataHelper();
			try
			{
				var isInsertSuccess = dataHelper.InsertRecord(data);
				return isInsertSuccess.ToString();
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		/// <summary>
		/// for deleting the records specified by the client
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]

		public string Delete(int id)
		{
			var dataHelper = new DataHelper();
			try
			{
				var isUpdateSuccess = dataHelper.DeleteRecord(id);
				return isUpdateSuccess.ToString();
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
	}
}