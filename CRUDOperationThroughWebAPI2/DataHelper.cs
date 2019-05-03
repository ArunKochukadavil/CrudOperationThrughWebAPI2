using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CRUDOperationThroughWebAPI2.Models;

namespace CRUDOperationThroughWebAPI2
{
	public class DataHelper
	{
		string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

		/// <summary>
		/// for getting records from the table
		/// </summary>
		/// <returns></returns>
		public List<UserData> GetValuesFromTable()
		{
			var userData = new List<UserData>();
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("Select * from [OdyContent_testDONOTUSEIT].[ODYSOLVD\\Arun.Kochukadavil].[UserDataDoNotUseIt]", con);
			con.Open();
			SqlDataReader data = cmd.ExecuteReader();
			while (data.Read())
			{
				userData.Add(
					new UserData()
					{
						Uid = data.GetInt32(0),
						FirstName = data.GetString(1),
						MiddleName = data.GetString(2),
						LastName = data.GetString(3),
						Address = data.GetString(4)
					}
				);
			}
			con.Close();
			return userData;
		}

		/// <summary>
		/// for updating the table with changes
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public bool UpdateUserTable(UserData data)
		{
			SqlConnection con = new SqlConnection(connectionString);
			con.Open();
			SqlCommand cmd = new SqlCommand("Update [OdyContent_testDONOTUSEIT].[ODYSOLVD\\Arun.Kochukadavil].[UserDataDoNotUseIt] set firstName=@firstName, middleName=@middleName, lastName=@middleName, Address=@address where uid=@uid", con);
			cmd.Parameters.AddWithValue("@firstName", data.FirstName);
			cmd.Parameters.AddWithValue("@middleName", data.MiddleName);
			cmd.Parameters.AddWithValue("@lastName", data.LastName);
			cmd.Parameters.AddWithValue("@address", data.Address);
			cmd.Parameters.AddWithValue("@uid", data.Uid);
			int count = cmd.ExecuteNonQuery();
			con.Close();
			return count > 0;
		}

		/// <summary>
		/// for inserting new records into the table
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public bool InsertRecord(UserData data)
		{
			SqlConnection con = new SqlConnection(connectionString);
			con.Open();
			SqlCommand cmd = new SqlCommand("Insert into [OdyContent_testDONOTUSEIT].[ODYSOLVD\\Arun.Kochukadavil].[UserDataDoNotUseIt] Values(@firstName,@middleName,@lastName,@address)", con);
			cmd.Parameters.AddWithValue("@firstName", data.FirstName);
			cmd.Parameters.AddWithValue("@middleName", data.MiddleName);
			cmd.Parameters.AddWithValue("@lastName", data.LastName);
			cmd.Parameters.AddWithValue("@address", data.Address);
			int count = cmd.ExecuteNonQuery();
			
			con.Close();
			return count > 0;
		}

		/// <summary>
		/// for deleting the records from the table
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool DeleteRecord(int id)
		{
			SqlConnection con = new SqlConnection(connectionString);
			con.Open();
			SqlCommand cmd = new SqlCommand("delete from [OdyContent_testDONOTUSEIT].[ODYSOLVD\\Arun.Kochukadavil].[UserDataDoNotUseIt] where uid=@uid", con);
			cmd.Parameters.AddWithValue("@uid", id);
			int count = cmd.ExecuteNonQuery();
			con.Close();
			return count > 0;
		}
	}
}