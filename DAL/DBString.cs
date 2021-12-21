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

        public static int InsertStudentDetails(StudentModel sModel)
        {
            SqlConnection connection = new SqlConnection(conStr);
            try
            {
                string query = "insert into StudentDetail (Name,DOB,DOJ,Address,City,Mobile) values (@Name,@DOB,@DOJ,@Address,@City,@Mobile);SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", sModel.Name);
                command.Parameters.AddWithValue("@DOB", sModel.DOB);
                command.Parameters.AddWithValue("@DOJ", sModel.DOJ);
                command.Parameters.AddWithValue("@Address", sModel.Address);
                command.Parameters.AddWithValue("@City", sModel.CityId);
                command.Parameters.AddWithValue("@Mobile", sModel.Mobile);
                connection.Open();
                var resp = command.ExecuteScalar();
                return Convert.ToInt32(resp);
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

        public static bool InsertStudentMarks(StudentModel sModel)
        {
            SqlConnection connection = new SqlConnection(conStr);
            try
            {
                string query = "insert into StudentMarks (Class,College,Obtaining,Obtained,StudentId) values (@Class,@College,@Obtaining,@Obtained,@StudentId);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Class", sModel.Class);
                command.Parameters.AddWithValue("@College", sModel.Clg);
                command.Parameters.AddWithValue("@Obtaining", sModel.Obtaining);
                command.Parameters.AddWithValue("@Obtained", sModel.Obtained);
                command.Parameters.AddWithValue("@StudentId", sModel.StudentId);
                connection.Open();
                var resp = command.ExecuteReader();
                return true;
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
        public static int GetStateId(string name)
        {
            SqlConnection conn = new SqlConnection(conStr);
            DataTable dt = new DataTable();
            int stateId = 0;
            try
            {
                string query = $"select code from CityMaster where name = '{name}'";
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                    stateId = Convert.ToInt32(dt.Rows[0]["code"].ToString());
                return stateId;
            }
            catch (Exception ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            return default;
        }

        public static void PullTest(string name)
        {
           
        }
    }
}