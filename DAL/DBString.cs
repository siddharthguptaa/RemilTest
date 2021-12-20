using DTestAssign.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DTestAssign.DAL
{
    public class DBString
    {
        public static string conStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        public static dynamic GetData()
        {
            SqlConnection conn = new SqlConnection(conStr);
            string query = "select * from CityMaster";
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public static List<string> GetCitiesName()
        {
            SqlConnection conn = new SqlConnection(conStr);
            DataTable dt = new DataTable();
            List<string> names = new List<string>();
            try
            {
                string query = "select Name from CityMaster";
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        names.Add(row["Name"].ToString());
                    }
                    return names;
                }
            }
            catch(Exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return default;
        }

        public static dynamic GetStateAndCountryNamebyCityName(string cityName)
        {
            SqlConnection conn = new SqlConnection(conStr);
            DataTable dt = new DataTable();
            try
            {
                string query = $"select StateMaster.Name as statename, countrymaster.name as countryname from CityMaster, StateMaster,CountryMaster where citymaster.Name = '{cityName}' and citymaster.statecode = statemaster.code and citymaster.countrycode = countrymaster.code";
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string sName = string.Empty;
                    string cName = string.Empty;
                    foreach (DataRow row in dt.Rows)
                    {
                        sName = (row["statename"].ToString());
                        cName = row["countryname"].ToString();
                    }
                    return new {sName = sName,cName = cName };
                }
            }
            catch (Exception)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return default;
        }

        public static bool InsertDetails(StudentModel sModel)
        {
            SqlConnection connection = new SqlConnection(conStr);
            try
            {
                string query = "insert into StudentDetail (Name,DOB,DOJ,Address,City,Mobile) values (@Name,@DOB,@DOJ,@Address,@City,@Mobile)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", sModel.Name);
                command.Parameters.AddWithValue("@DOB", sModel.DOB);
                command.Parameters.AddWithValue("@DOJ", sModel.DOJ);
                command.Parameters.AddWithValue("@Address", sModel.Address);
                command.Parameters.AddWithValue("@City", sModel.CityId);
                command.Parameters.AddWithValue("@Mobile", sModel.Mobile);
                connection.Open();
                var resp = command.ExecuteReader();

            }
            catch (Exception ex)
            {
                connection.Close();
            }
            finally
            {
                connection.Close();
            }
            return default;
        }
    }
}