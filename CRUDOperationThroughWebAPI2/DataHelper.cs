﻿using System;
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
		public List<UserData> GetValuesFromTable()
		{
			var userData = new List<UserData>();
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("Select * from UserDataDoNotUseIt", con);
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
		public bool UpdateUserTable(UserData data)
		{
			SqlConnection con = new SqlConnection(connectionString);
			con.Open();
			SqlCommand cmd = new SqlCommand("Update UserDataDoNotUseIt set firstName=@firstName, middleName=@middleName, lastName=@middleName, Address=@address where uid=@uid", con);
			cmd.Parameters.AddWithValue("@firstName", data.FirstName);
			cmd.Parameters.AddWithValue("@middleName", data.MiddleName);
			cmd.Parameters.AddWithValue("@lastName", data.LastName);
			cmd.Parameters.AddWithValue("@address", data.Address);
			cmd.Parameters.AddWithValue("@uid", data.Uid);
			int count = cmd.ExecuteNonQuery();
			con.Close();
			return count > 0;

		}
		public bool InsertRecord(UserData data)
		{
			SqlConnection con = new SqlConnection(connectionString);
			con.Open();
			SqlCommand cmd = new SqlCommand("Insert into UserDataDoNotUseIt Values(@firstName,@middleName,@lastName,@address)", con);
			cmd.Parameters.AddWithValue("@firstName", data.FirstName);
			cmd.Parameters.AddWithValue("@middleName", data.MiddleName);
			cmd.Parameters.AddWithValue("@lastName", data.LastName);
			cmd.Parameters.AddWithValue("@address", data.Address);
			int count = cmd.ExecuteNonQuery();
			con.Close();
			return count > 0;
		}
		public bool DeleteRecord(UserData data)
		{
			SqlConnection con = new SqlConnection(connectionString);
			con.Open();
			SqlCommand cmd = new SqlCommand("delete from UserDataDoNotUseIt where uid=@uid", con);
			cmd.Parameters.AddWithValue("@uid", data.Uid);
			int count = cmd.ExecuteNonQuery();
			con.Close();
			return count > 0;
		}
	}
}